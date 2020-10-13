using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProcess
{
    class Program
    {
        static void Main(string[] args)
        {
 
            ProcessStartInfo process1 = new ProcessStartInfo();
            process1.FileName = "C:/Users/seda8/Source/Repos/TestProcess/ProcessHomeWork/bin/Debug/ProcessHomeWork.exe";
            process1.Arguments = "6 2 *";
            Process.Start(process1);

            Console.ReadLine();
        }
    }
}
