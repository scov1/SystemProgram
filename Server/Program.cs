using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Server
{
    class Program
    {
        private static int remotePort = 8005;
        private static int localPort = 8006;
        static List<Client> clients = new List<Client>();


        [STAThread]
        static void Main(string[] args)
        {
            using (FileStream fs = new FileStream("base.json", FileMode.OpenOrCreate))
            {
                fs.Close();
                using (StreamReader sr = new StreamReader("base.json", System.Text.Encoding.Default, true))
                {
                    string line = sr.ReadToEnd();
                    if (line.Length > 0)
                        clients = JsonConvert.DeserializeObject<List<Client>>(line);
                }
            }
            if (clients == null)
            {
                clients.Add(new Client("test", "test", "test"));
                Console.WriteLine("База пустая");
            }
            clients.ForEach(i => i.IsConnect = false);
            SaveBase();
            try
            {
                Thread tRec = new Thread(ReceiverAsync);
                tRec.Start();
                while (true) { }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникло исключение: " + ex.ToString() + "\n  " + ex.Message);
            }
        }

        public static void SaveBase()
        {
            string db = JsonConvert.SerializeObject(clients);
            File.Delete("base.json");
            File.WriteAllText("base.json",db);

        }
        private static void SendMessage(Message message)
        {
            Socket socket = new Socket("msg", message);
            UdpClient sender = new UdpClient();

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(message.Recipient.IpAddress), remotePort);

            try
            {
                string serialized = JsonConvert.SerializeObject(socket);
                byte[] bytes = Encoding.UTF8.GetBytes(serialized);

                sender.Send(bytes, bytes.Length, endPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникло исключение: " + ex.ToString() + "\n  " + ex.Message);
            }
            finally
            {
                sender.Close();
            }
        }

        private static void SendMessageGroop(Groops groops)
        {
            Console.WriteLine(groops.Message);
            Socket socket = new Socket("groupMsg", groops);
            foreach (Client cl in groops.Clients)
            {
                if (cl != groops.Sender && cl.IsConnect==true)
                {
                    UdpClient sender = new UdpClient();
                    IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(cl.IpAddress), remotePort);

                    try
                    {
                        string serialized = JsonConvert.SerializeObject(socket);
                        byte[] bytes = Encoding.UTF8.GetBytes(serialized);

                        sender.Send(bytes, bytes.Length, endPoint);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Возникло исключение: " + ex.ToString() + "\n  " + ex.Message);
                    }
                    finally
                    {
                        sender.Close();
                    }
                }
            }
        }

        private static void SendClient(Client client)
        {
            Socket socket = new Socket("client", client);
            UdpClient sender = new UdpClient();

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(client.IpAddress), remotePort);

            try
            {
                string serialized = JsonConvert.SerializeObject(socket);
                byte[] bytes = Encoding.UTF8.GetBytes(serialized);

                sender.Send(bytes, bytes.Length, endPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникло исключение: " + ex.ToString() + "\n  " + ex.Message);
            }
            finally
            {
                sender.Close();
            }
        }

        private static void SendFile(FileSend fs)
        {
            Console.WriteLine("Отправка файла {0} из {1}",fs.ThisBlock,fs.AllBlocks);
            Socket socket = new Socket("file", fs);
            UdpClient sender = new UdpClient();

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(fs.Recipient.IpAddress), remotePort);

            try
            {
                string serialized = JsonConvert.SerializeObject(socket);
                byte[] bytes = Encoding.UTF8.GetBytes(serialized);

                sender.Send(bytes, bytes.Length, endPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникло исключение: " + ex.ToString() + "\n  " + ex.Message);
            }
            finally
            {
                sender.Close();
            }
        }

        private static void SendOnline()
        {
            Socket socket = new Socket("clientsOnline", clients.FindAll(i=>i.IsConnect==true));
 

            foreach (Client item in clients.FindAll(i => i.IsConnect == true)) {
                UdpClient sender = new UdpClient();
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(item.IpAddress), remotePort);

                try
                {
                    string serialized = JsonConvert.SerializeObject(socket);
                    byte[] bytes = Encoding.UTF8.GetBytes(serialized);

                    sender.Send(bytes, bytes.Length, endPoint);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Возникло исключение: " + ex.ToString() + "\n  " + ex.Message);
                }
                finally
                {
                    sender.Close();
                }
            }
        }

        public static void SendMail(string fromMailLogin, string fromMailPassword, string toMailLogin)
        {
            MailAddress from = new MailAddress(fromMailLogin);
            MailAddress to = new MailAddress(toMailLogin);
            MailMessage mail = new MailMessage(from, to);
            mail.Subject = "Письмо";
            mail.Body = "Вы успешно авторизовались в чате :)";
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(fromMailLogin, fromMailPassword);
            smtp.EnableSsl = true;
            smtp.Send(mail);

        }
        public static async void ReceiverAsync()
        {
            UdpClient receivingUdpClient = new UdpClient(localPort);

            try
            {
                while (true)
                {
                    UdpReceiveResult udpReceiveResult = await receivingUdpClient.ReceiveAsync();
                    byte[] receiveBytes = udpReceiveResult.Buffer;
                    if (udpReceiveResult.RemoteEndPoint!=null)
                    {
                        Socket socket = JsonConvert.DeserializeObject<Socket>(Encoding.UTF8.GetString(receiveBytes));
                        if (socket.Type.Equals("msg"))
                        {
                            SendMessage(JsonConvert.DeserializeObject<Message>(socket.Obj.ToString()));
                        }

                        if (socket.Type.Equals("groupMsg"))
                        {
                            SendMessageGroop(JsonConvert.DeserializeObject<Groops>(socket.Obj.ToString()));
                        }

                        if (socket.Type.Equals("exit"))
                        {
                            Client cl = JsonConvert.DeserializeObject<Client>(socket.Obj.ToString());
                            Console.WriteLine("Disconnect: {0} | Ip: {1}", cl.Name, cl.IpAddress);
                            clients.FirstOrDefault(i => i.Name.Equals(cl.Name)).IsConnect = false;
                            SaveBase();
                        }

                        if (socket.Type.Equals("file"))
                        {
                            SendFile(JsonConvert.DeserializeObject<FileSend>(socket.Obj.ToString()));
                        }

                        if (socket.Type.Equals("logon"))
                        {
                            SendMail("seda888892@gmail.com", "*", "seda888892@gmail.com");
                            LogOnForm logon = JsonConvert.DeserializeObject<LogOnForm>(socket.Obj.ToString());
                            Console.WriteLine(logon.Password+" "+logon.Name);
                            if (clients.FirstOrDefault(i => i.Password == logon.Password && i.Name == logon.Name)==null)
                            {
                                Client newClient = new Client(logon.Name, logon.Password, logon.IpAddress);
                                newClient.IsConnect = true;
                                clients.Add(newClient);
                                SaveBase();
                                Console.WriteLine("Regisctrate: {0} | Ip: {1}",logon.Name,logon.IpAddress);
                                SendClient(newClient);
                                SendOnline();
                            }
                            else
                            {
                                clients.FirstOrDefault(i => i.Password == logon.Password && i.Name == logon.Name).IsConnect = true;
                                clients.FirstOrDefault(i => i.Password == logon.Password && i.Name == logon.Name).IpAddress = logon.IpAddress;
                                Client client = clients.FirstOrDefault(i => i.Password == logon.Password && i.Name == logon.Name);
                                Console.WriteLine("Connect: {0} | Ip: {1}", logon.Name, logon.IpAddress);
                                SendClient(client);
                                SendOnline();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникло исключение: " + ex.ToString() + "\n  " + ex.Message);
            }
        }
    }
}

