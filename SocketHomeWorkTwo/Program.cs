using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
	class Program
	{
        private const string IP = "127.0.0.1";
        private const int Port = 12000;
        static void Main(string[] args)
		{
            TcpListener server = null;
            try
            {
                server = new TcpListener(IPAddress.Parse(IP), Port);
                server.Start();// запуск слушателя
                while (true)
                {
                    Console.WriteLine("Waiting...");
            
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connection with Client");
                    Console.WriteLine("Connection was time => " + DateTime.Now);
                    NetworkStream stream = client.GetStream();
                    string response = "Currency your money " ;
                    byte[] data = Encoding.UTF8.GetBytes(response);
                    stream.Write(data, 0, data.Length);

                    Console.WriteLine($"Sent Message: {response}");
       
                    stream.Close();
                    client.Close();
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (server != null)
                    server.Stop();
                Console.ReadLine();
            }
        }
	}
}
