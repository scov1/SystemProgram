using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
	class Program
	{
		static void Main(string[] args)
		{
			const string ip = "192.168.102.23";
			const int port = 8080;

			IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
			Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

			Console.WriteLine("Input str:");
			string str = Console.ReadLine();

			var data = Encoding.UTF8.GetBytes(str);
			socket.Connect(ep);
			socket.Send(data);

			byte[] buffer = new byte[1024];
			StringBuilder sbAnswer = new StringBuilder();
			int i=0;
			do
			{
				i = socket.Receive(buffer);
				sbAnswer.Append(Encoding.UTF8.GetString(buffer, 0, i));
			} while (socket.Available > 0);

			Console.WriteLine(sbAnswer.ToString());

			socket.Shutdown(SocketShutdown.Both);
			socket.Close();


			Console.ReadLine();
		}
	}
}
