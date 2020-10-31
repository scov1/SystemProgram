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
        static readonly byte[] CONN_STOP = Encoding.ASCII.GetBytes("Server is busy, please try later :(");
        static void Main(string[] args)
		{
            int conn = 0;
            TcpListener server = null;
            Console.WriteLine("Input max connects client");
            int count = Convert.ToInt32(Console.ReadLine());
            try
            {
                server = new TcpListener(IPAddress.Parse(IP), Port);
                server.Start();// запуск слушателя
                while (true)
                {
                    Console.WriteLine("Waiting...");
                    TcpClient client = server.AcceptTcpClient();

                    if (conn >= count)
                    {
                        //   TcpClient client = server.AcceptTcpClient();
                        NetworkStream stream = client.GetStream();
                        string response = "Server is busy, please try later :(";
                        byte[] data = Encoding.UTF8.GetBytes(response);
                        stream.Write(data, 0, data.Length);
                        Console.WriteLine($"Sent Message: {response}");
                        //string response = "Server is busy,please try later :(";
                        //byte[] data = Encoding.UTF8.GetBytes(response);
                        //stream.Write(data, 0, data.Length);
                        //Console.WriteLine($"Sent Message: {response}");
                        //stream.Close();
                        stream.Close();
                        client.Close();

                    }
                    else
                    {
                      
                        Console.WriteLine("Connection with Client");
                        Console.WriteLine("Connection was time => " + DateTime.Now);
                        conn++;
                        NetworkStream stream = client.GetStream();
                        string response = "Everythings it's all ok";
                        byte[] data = Encoding.UTF8.GetBytes(response);
                        stream.Write(data, 0, data.Length);
                        Console.WriteLine($"Sent Message: {response}");
                        stream.Close();
                        client.Close();
                    }

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
