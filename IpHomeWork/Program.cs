using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace IpHomeWork
{
	class Program
	{
		static void Main(string[] args)
		{

			string host = Dns.GetHostName();
			string ipAdress = Dns.GetHostByName(host).AddressList[0].ToString();
			Console.WriteLine("my IP => " + ipAdress);


			foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
			{
					var macAdress = BitConverter.ToString(nic.GetPhysicalAddress().GetAddressBytes());
					Console.WriteLine("my MAC => " + macAdress);
					break;
			}

			Ping ping = new Ping();
			PingReply reply = ping.Send(ipAdress);
			try
			{
				if (reply.Status == IPStatus.Success)
				{
					Console.WriteLine("Status: " + reply.Status + " \nTime: " + reply.RoundtripTime.ToString());

				}

			}catch
			{
				Console.WriteLine("ERROR!");
			}

			Console.ReadLine();
		}
	}
}
