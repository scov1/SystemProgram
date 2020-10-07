using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Windows;

namespace HomeWork6
{
	class Program
	{

		static void Main(string[] args)
		{
			if (Process.GetProcessesByName("HomeWork6").Length > 3)
			{
				Console.WriteLine("You cannot create more than 3 copies");
			}
            else
            {
				Console.WriteLine("Working!");
            }

			Console.ReadKey();
		}
	}
}
