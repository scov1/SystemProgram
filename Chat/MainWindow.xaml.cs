using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Threading;
using System.Windows.Threading;
using System.IO;
using Microsoft.Win32;

namespace Chat
{
    public partial class MainWindow : Window
    {
        public static int localPort = 8005;
        public static int remotePort = 8006;
        public static string host = "127.0.0.1";
        public static Client client = null;
        public static List<Client> clientsOnline = null;
        public static List<FileRead> files = new List<FileRead>();
        public List<Groops> groops = new List<Groops>();

        Thread thread;
        public MainWindow()
        {
            InitializeComponent();
            thread = new Thread(ReceiverAsync);
            thread.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string myHost = Dns.GetHostName();
            var ip = System.Net.Dns.GetHostByName(myHost).AddressList[0].ToString();
            LogOnForm logon = new LogOnForm(loginBox.Text, passwordBox.Text);
            logon.IpAddress = ip;
            host = ipAddressBox.Text;
            Socket socket = new Socket("logon", logon);
            SendSocket(socket);
        }

        private  static void SendSocket(Socket socket)
        {
            UdpClient sender = new UdpClient();
            IPAddress hostConnect;
            IPAddress.TryParse(host, out hostConnect);
            if (hostConnect == null)
            {
                MessageBox.Show("Неправильно указан адрес сервера!");
            }
            else
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(host), remotePort);
                try
                {
                    string serialized = JsonConvert.SerializeObject(socket);
                    byte[] bytes = Encoding.UTF8.GetBytes(serialized);
                    sender.Send(bytes, bytes.Length, endPoint);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Возникло исключение: " + ex.ToString() + "\n  " + ex.Message);
                }
                finally
                {
                    sender.Close();
                }
            }
        }

        public void LoadContent(Client cl)
        {
            client = cl;
            LogonMenu.Visibility = Visibility.Hidden;
            Content.Visibility = Visibility.Visible;
            NameLabel.Content = client.Name;
            groopList.Items.Add(new Label() { Foreground = Brushes.White, Content = "Количество груп > " + groops.Count });
            foreach (Groops gr in groops) {
                groopList.Items.Add(new Label() { Content = gr.Name, DataContext = gr });
            }
        }

        public void LoadOnline(List<Client> cls)
        {
            clientsOnline = cls;
            FriendList.Items.Clear();
            FriendList.Items.Add(new Label() { Foreground = Brushes.White, Content = "Онлайн > " + cls.Count });
            foreach (Client item in cls)
            {
                FriendList.Items.Add(new Label() { Content = item.Name });
                foreach (Groops gr in groops)
                {
                    foreach (Client cl in gr.Clients)
                    {
                        if (cl.IsConnect == true && item.IsConnect == false && cl.Name.Equals(item.Name))
                        {
                            gr.Clients.FirstOrDefault(i => i.Name.Equals(cl.Name)).IsConnect = false;
                        }
                    }
                }
            }


        }

        public async void ReceiverAsync()
        {
            UdpClient receivingUdpClient = new UdpClient(localPort);

            try
            {
                while (true)
                {
                    UdpReceiveResult udpReceiveResult = await receivingUdpClient.ReceiveAsync();
                    byte[] receiveBytes = udpReceiveResult.Buffer;
                    if (udpReceiveResult.Buffer.Length > 0)
                    {
                        Socket socket = JsonConvert.DeserializeObject<Socket>(Encoding.UTF8.GetString(receiveBytes));

                        if (socket.Type.Equals("client"))
                        {
                            await Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                              (ThreadStart)delegate ()
                                   {
                                       LoadContent(JsonConvert.DeserializeObject<Client>(socket.Obj.ToString()));
                                   }
                            );

                        }

                        if (socket.Type.Equals("clientsOnline"))
                        {
                            await Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                              (ThreadStart)delegate ()
                              {
                                  LoadOnline(JsonConvert.DeserializeObject<List<Client>>(socket.Obj.ToString()));
                              }
                            );

                        }

                        if (socket.Type.Equals("msg"))
                        {
                            await Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                              (ThreadStart)delegate ()
                              {
                                  ViewMessage(JsonConvert.DeserializeObject<Message>(socket.Obj.ToString()));
                              });

                        }

                        if (socket.Type.Equals("file"))
                        {
                            await Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                              (ThreadStart)delegate ()
                              {
                                  LoadFile(JsonConvert.DeserializeObject<FileSend>(socket.Obj.ToString()));
                              });

                        }

                        if (socket.Type.Equals("groupMsg"))
                        {
                            await Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                              (ThreadStart)delegate ()
                              {
                                  ViewMessageGroop(JsonConvert.DeserializeObject<Groops>(socket.Obj.ToString()));
                              });

                        }

                        if (socket.Type.Equals("error"))
                        {

                        }

                        socket = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникло исключение: " + ex.ToString() + "\n  " + ex.Message);
            }
        }

        public void LoadFile(FileSend fileSend)
        {
            if (!files.Any(i => i.Name == fileSend.Name && i.Expansion == fileSend.Expansion))
            {
                FileRead read = new FileRead(fileSend.Sender,fileSend.Recipient,fileSend.Name, fileSend.Expansion, fileSend.Type);
                read.UploadData(fileSend.Data);
                files.Add(read);
            }
            else
            {
                if (fileSend.ThisBlock != fileSend.AllBlocks)
                {
                    files.FirstOrDefault(i => i.Name == fileSend.Name && i.Expansion == fileSend.Expansion).UploadData(fileSend.Data);
                }
                else
                {
                    files.FirstOrDefault(i => i.Name == fileSend.Name && i.Expansion == fileSend.Expansion).UploadData(fileSend.Data);
                    FileRead read = files.FirstOrDefault(i => i.Name == fileSend.Name && i.Expansion == fileSend.Expansion);
                    ViewMessageFile(read);
                }
            }
        }
    

        public void ViewMessage(Message message)
        {
            bool check = false;
            foreach (TabItem item in tabControl.Items)
            {
                if (item.Header.Equals(message.Sender.Name))
                {
                    check = true;
                    StackPanel sp = item.Content as StackPanel;
                    ListView lv = sp.Children[0] as ListView;
                    lv.Items.Add(new Label() { Content = message.Sender.Name + " > " + message.MessageText });
                }
            }
            if (!check)
            {
                TabItem tabItem = new TabItem() { Header = message.Sender.Name };
                StackPanel sp = new StackPanel() { HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch };
                ListView viewMessage = new ListView() { Width = 350, Height = 170, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Top };
                TextBox sendMessage = new TextBox() { Width = 350, Height = 70, Margin = new Thickness(0, 20, 0, 10), HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Center };
                StackPanel spbtn = new StackPanel() { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Stretch };
                Button sendButton = new Button() { Content="Отправить сообщение", Width = 150, Height = 30, VerticalAlignment = VerticalAlignment.Bottom , Margin=new Thickness(0,0,40,0) };
                sendButton.DataContext = clientsOnline.FirstOrDefault(i => i.Name.Equals(tabItem.Header));
                sendButton.Click += SendButton_Click;
                Button sendFile = new Button() { Content = "Отправить файл", Width = 150, Height = 30, VerticalAlignment = VerticalAlignment.Bottom ,Margin = new Thickness(40, 0, 0, 0)};
                sendFile.DataContext = clientsOnline.FirstOrDefault(i => i.Name.Equals(tabItem.Header));
                sendFile.Click += SendFile_Click;

                viewMessage.Items.Add(new Label() { Content = message.Sender.Name + " > " + message.MessageText });
                sp.Children.Add(viewMessage);
                sp.Children.Add(sendMessage);
                spbtn.Children.Add(sendButton);
                spbtn.Children.Add(sendFile);
                sp.Children.Add(spbtn);
                tabItem.Content = sp;
                tabControl.Items.Add(tabItem);
            }
        }

        private void SendFile_Click(object sender, RoutedEventArgs e)
        {
            Client rec = (sender as Button).DataContext as Client;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == false)
                return;
            Button btn = sender as Button;
            StackPanel sp2 = btn.Parent as StackPanel;
            StackPanel sp = sp2.Parent as StackPanel;
            ListView listView = sp.Children[0] as ListView;
            string filename = openFileDialog.FileName;
            string[] name = openFileDialog.SafeFileName.Split('.');
            listView.Items.Add(new Label() { Content = openFileDialog.SafeFileName + " отправлен!" });

            byte[] data = System.IO.File.ReadAllBytes(filename);
            FileRead fr = new FileRead(client, rec, name[0], name[1], "file");
            fr.Data = data;

            Thread th = new Thread(SendFile);
            th.Start(fr);
        }

        public void SendFile(object obj)
        {
            FileRead fr = obj as FileRead;
            foreach (FileSend fs in fr.GetListFileSend())
            {
                Socket socket = new Socket("file", fs);
                SendSocket(socket);
            }
        }
      
        public void ViewMessageFile(FileRead fileRead)
        {
            bool check = false;
            foreach (TabItem item in tabControl.Items)
            {
                if (item.Header.Equals(fileRead.Sender.Name))
                {
                    check = true;
                    StackPanel sp = item.Content as StackPanel;
                    ListView lv = sp.Children[0] as ListView;
                    Button btn = new Button() { Content = fileRead.Name +"."+ fileRead.Expansion };
                    btn.DataContext = fileRead;
                    btn.Click += Btn_Click;
                    lv.Items.Add(new Label() { Content = fileRead.Sender.Name });
                    lv.Items.Add(btn);
                }
            }
            if (!check)
            {
                TabItem tabItem = new TabItem() { Header = fileRead.Sender.Name };
                StackPanel sp = new StackPanel() { HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch };
                ListView viewMessage = new ListView() { Width = 350, Height = 170, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Top };
                TextBox sendMessage = new TextBox() { Width = 350, Height = 70, Margin = new Thickness(0, 20, 0, 10), HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Center };
                StackPanel spbtn = new StackPanel() { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Stretch };
                Button sendButton = new Button() { Content = "Отправить сообщение", Width = 150, Height = 30, VerticalAlignment = VerticalAlignment.Bottom, Margin = new Thickness(0, 0, 40, 0) };
                sendButton.DataContext = clientsOnline.FirstOrDefault(i => i.Name.Equals(tabItem.Header));
                sendButton.Click += SendButton_Click;
                Button sendFile = new Button() { Content = "Отправить файл", Width = 150, Height = 30, VerticalAlignment = VerticalAlignment.Bottom, Margin = new Thickness(40, 0, 0, 0) };
                sendFile.DataContext = clientsOnline.FirstOrDefault(i => i.Name.Equals(tabItem.Header));
                sendFile.Click += SendFile_Click;
                Button btn = new Button() { Content = fileRead.Name + fileRead.Expansion };
                btn.DataContext = fileRead;
                viewMessage.Items.Add(new Label() { Content = fileRead.Sender.Name });
                viewMessage.Items.Add(btn);
                sp.Children.Add(viewMessage);
                sp.Children.Add(sendMessage);
                spbtn.Children.Add(sendButton);
                spbtn.Children.Add(sendFile);
                sp.Children.Add(spbtn);
                tabItem.Content = sp;
                tabControl.Items.Add(tabItem);
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            FileRead fr = btn.DataContext as FileRead;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "." + fr.Expansion;
            if (saveFileDialog.ShowDialog() == false)
                return;
         
            string filename = saveFileDialog.FileName;
        
            System.IO.File.WriteAllBytes(filename, fr.Data);
            MessageBox.Show("Файл сохранен");
        }

        public void ViewMessageGroop(Groops groop)
        {
            if (!groop.Sender.IpAddress.Equals(client.IpAddress))
            {
                bool check = false;
                foreach (TabItem item in tabControl.Items)
                {
                    if (item.Header.Equals(groop.Name))
                    {
                        check = true;
                        StackPanel sp = item.Content as StackPanel;
                        ListView lv = sp.Children[0] as ListView;
                        lv.Items.Add(new Label() { Content = groop.Sender.Name + " > " + groop.Message });
                    }
                }
                if (!check)
                {
                    TabItem tabItem = new TabItem() { Header = groop.Name };
                    StackPanel sp = new StackPanel() { HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch };
                    ListView viewMessage = new ListView() { Width = 350, Height = 170, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Top };
                    TextBox sendMessage = new TextBox() { Width = 350, Height = 70, Margin = new Thickness(0, 20, 0, 10), HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Center };
                    Button sendButtonGroop = new Button() { Content = "Отправить сообщение", Width = 150, Height = 30, VerticalAlignment = VerticalAlignment.Bottom };
                    sendButtonGroop.DataContext = groop;
                    sendButtonGroop.Click += SendButtonGroop_Click;
                    sp.Children.Add(viewMessage);
                    sp.Children.Add(sendMessage);
                    sp.Children.Add(sendButtonGroop);
                    tabItem.Content = sp;
                    tabControl.Items.Add(tabItem);
                    groops.Add(groop);
                    groopList.Items.Clear();
                    groopList.Items.Add(new Label() { Foreground = Brushes.White, Content = "Количество груп > " + groops.Count });
                    foreach (Groops gr in groops)
                    {
                        groopList.Items.Add(new Label() { Content = gr.Name, DataContext = gr });
                    }
                }
            }
        }
        private void FriendList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (FriendList.SelectedIndex >= 1)
            {
                
                TabItem tabItem = new TabItem() { Header = (FriendList.SelectedItem as Label).Content };
                StackPanel sp = new StackPanel() { HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch };
                ListView viewMessage = new ListView() { Width = 350, Height=170, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Top };
                TextBox sendMessage = new TextBox() { Width = 350, Height = 70, Margin = new Thickness(0, 20, 0, 10), HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Center };
                StackPanel spbtn = new StackPanel() { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Stretch };
                Button sendButton = new Button() { Content = "Отправить сообщение", Width = 150, Height = 30, VerticalAlignment = VerticalAlignment.Bottom, Margin = new Thickness(0, 0, 40, 0) };
                sendButton.DataContext = clientsOnline.FirstOrDefault(i => i.Name.Equals(tabItem.Header));
                sendButton.Click += SendButton_Click;
                Button sendFile = new Button() { Content = "Отправить файл", Width = 150, Height = 30, VerticalAlignment = VerticalAlignment.Bottom, Margin = new Thickness(40, 0, 0, 0) };
                sendFile.DataContext = clientsOnline.FirstOrDefault(i => i.Name.Equals(tabItem.Header));
                sendFile.Click += SendFile_Click;
                sp.Children.Add(viewMessage);
                sp.Children.Add(sendMessage);
                spbtn.Children.Add(sendButton);
                spbtn.Children.Add(sendFile);
                sp.Children.Add(spbtn);
                tabItem.Content = sp;
                bool check = false;
                foreach(TabItem item in tabControl.Items)
                {
                    if (item.Header.Equals(tabItem.Header))
                    {
                        check = true;
                        tabControl.SelectedIndex = tabControl.Items.IndexOf(item);
                    }
                }
                if (!check)
                {
                    tabControl.Items.Add(tabItem);
                    tabControl.SelectedIndex = tabControl.Items.IndexOf(tabItem);
                }
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Client friend = btn.DataContext as Client;
            StackPanel sp2 = btn.Parent as StackPanel;
            StackPanel sp = sp2.Parent as StackPanel;
            TextBox msg = sp.Children[1] as TextBox;
            ListView listView = sp.Children[0] as ListView;
            if (msg.Text.Length > 0)
            {
                Message message = new Message(client, friend, msg.Text, DateTime.Now);
                listView.Items.Add(new Label() { Content = message.Sender.Name + " > " + message.MessageText });
                Socket socket = new Socket("msg", message);
                SendSocket(socket);
                msg.Text = "";
            }
        }

        private void CreateGroopBtn_Click(object sender, RoutedEventArgs e)
        {
            Content.Visibility = Visibility.Hidden;
            groupMenu.Visibility = Visibility.Visible;         
            foreach(Client cl in clientsOnline)
            {
                if(!cl.Name.Equals(client.Name))
                friendBox.Items.Add(new Label() { Content = cl.Name, DataContext = cl });
            }
        }

        private void ExitGroupMenu_Click(object sender, RoutedEventArgs e)
        {
            Content.Visibility = Visibility.Visible;
            groupMenu.Visibility = Visibility.Hidden;
            friendBox.Items.Clear();
        }

        private void CreateGroup_Click(object sender, RoutedEventArgs e)
        {
            Groops groop = new Groops(groupNameBox.Text, client);
            foreach(Label lb in friendBox.Items)
            {
                if(!client.Name.Equals((lb.DataContext as Client).Name))
                groop.AddGrpoop(lb.DataContext as Client);
            }
            groop.AddGrpoop(client);
            groops.Add(groop);
            
            TabItem tabItem = new TabItem() { Header = groop.Name };
            StackPanel sp = new StackPanel() { HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch };
            ListView viewMessage = new ListView() { Width = 350, Height = 170, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Top };
            TextBox sendMessage = new TextBox() { Width = 350, Height = 70, Margin=new Thickness(0,20,0,10), HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Center };
            Button sendButtonGroop = new Button() {Content="Отправить сообщение", Width = 150, Height = 30, VerticalAlignment = VerticalAlignment.Bottom };
            sendButtonGroop.DataContext = groop;
            sendButtonGroop.Click += SendButtonGroop_Click;
            sp.Children.Add(viewMessage);
            sp.Children.Add(sendMessage);
            sp.Children.Add(sendButtonGroop);
            tabItem.Content = sp;
            bool check = false;
            foreach (TabItem item in tabControl.Items)
            {
                if (item.Header.Equals(tabItem.Header))
                {
                    check = true;
                }
            }
            if (!check)
            {
                tabControl.Items.Add(tabItem);
                groopList.Items.Clear();
                groopList.Items.Add(new Label() { Foreground=Brushes.White, Content = "Количество груп > " + groops.Count });
                foreach (Groops gr in groops)
                {
                    groopList.Items.Add(new Label() { Content = gr.Name, DataContext = gr });
                }
            }
            Content.Visibility = Visibility.Visible;
            groupMenu.Visibility = Visibility.Hidden;
            friendBox.Items.Clear();
        }

        private void SendButtonGroop_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Groops group = btn.DataContext as Groops;
            StackPanel sp = btn.Parent as StackPanel;
            TextBox msg = sp.Children[1] as TextBox;
            group.Sender = client;
            ListView listView = sp.Children[0] as ListView;
            if (msg.Text.Length > 0)
            {
                group.Message = msg.Text;
                listView.Items.Add(new Label() { Content = group.Sender.Name + " > " + group.Message });
                Socket socket = new Socket("groupMsg", group);
                SendSocket(socket);
                msg.Text = "";
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Socket socket = new Socket("exit", client);
            SendSocket(socket);
            Thread.Sleep(2000);
            if (thread != null)
            {
                thread.Abort();
            }
        }

        private void GroopList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (groopList.SelectedIndex >= 1)
            {

                TabItem tabItem = new TabItem() { Header = (groopList.SelectedItem as Label).Content };
                StackPanel sp = new StackPanel() { HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch };
                ListView viewMessage = new ListView() { Width = 350, Height = 170, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Top };
                TextBox sendMessage = new TextBox() { Width = 350, Height = 70, Margin = new Thickness(0, 20, 0, 10), HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Center };
                Button sendButtonGroop = new Button() {Content="Отправить сообщение", Width = 150, Height = 30, VerticalAlignment = VerticalAlignment.Bottom };
                sendButtonGroop.DataContext = (groopList.SelectedItem as Label).DataContext as Groops;
                sendButtonGroop.Click += SendButtonGroop_Click;
                sp.Children.Add(viewMessage);
                sp.Children.Add(sendMessage);
                sp.Children.Add(sendButtonGroop);
                tabItem.Content = sp;
                bool check = false;
                foreach (TabItem item in tabControl.Items)
                {
                    if (item.Header.Equals(tabItem.Header))
                    {
                        check = true;
                        tabControl.SelectedIndex = tabControl.Items.IndexOf(item);
                    }
                }
                if (!check)
                    tabControl.Items.Add(tabItem);
            }
        }
    }
}

