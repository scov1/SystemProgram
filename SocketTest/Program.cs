using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace SocketTest
{
	class Program
	{
	
		static void Main(string[] args)
		{
			const string ip = "192.168.102.23";
			const int port = 8080;
			IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
			Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
			socket.Bind(ep);
			socket.Listen(5);

			
			while (true)
			{
				var i = socket.Accept();
				byte[] buffer = new byte[1024];
				StringBuilder sb = new StringBuilder();
				int size = 0;
				
				do
				{
					size = i.Receive(buffer);
					sb.Append(Encoding.UTF8.GetString(buffer, 0, size));
				} while (i.Available > 0);

				Console.WriteLine(sb);

				i.Shutdown(SocketShutdown.Both);
				i.Close();
			}
		}
	}
}
