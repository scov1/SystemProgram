using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
	public partial class Form1 : Form
	{
        private const string IP = "127.0.0.1";
        private const int Port = 12000;
        public Form1()
		{
			InitializeComponent();
			currency_one.SelectedIndex = 0;
			currency_two.SelectedIndex = 1;
		}

		private void btn_convert_click(object sender, EventArgs e)
		{
            string from, to;
            string response = "";
            from = currency_one.SelectedItem.ToString();
            to = currency_two.SelectedItem.ToString();

            try
            {
                TcpClient client = new TcpClient();
                client.Connect(IP, Port);

                byte[] data = new byte[256];
                NetworkStream stream = client.GetStream();

                do
                {
                    int bytes = stream.Read(data, 0, data.Length);
                    response = (Encoding.UTF8.GetString(data, 0, bytes));
                }
                while (stream.DataAvailable);

                    webBrowser1.Navigate("https://www.google.ru/search?q=" + count_money.Text+  "" + from + " %D0%B2 " + to);
                

                // Закрываем потоки
                stream.Close();
                client.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }

           
        }
	}
}
