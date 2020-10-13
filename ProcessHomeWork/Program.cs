using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessHomeWork
{
    class Program
    {
        static void Main(string[] args) 

        {
                var num1 = args[0];
                var num2 = args[1];
                var checkOperation = args[2];
           

                if (checkOperation == "+")
                {
                    int res = Convert.ToInt32(num1) + Convert.ToInt32(num2);
                    Console.WriteLine($"{num1} + {num2} = " + res );
                }
                else if (checkOperation == "-")
                {
                    int res = Convert.ToInt32(num1) - Convert.ToInt32(num2);
                    Console.WriteLine($"{num1} - {num2} = " + res);
                }
                else if (checkOperation == "/")
                {
                    int res = Convert.ToInt32(num1) / Convert.ToInt32(num2);
                    Console.WriteLine($"{num1} / {num2} = " + res);
                }
                else if (checkOperation == "*")
                {
                    int res = Convert.ToInt32(num1) * Convert.ToInt32(num2);
                    Console.WriteLine($"{num1} * {num2} = " + res);
                }
            Console.ReadLine();
        }
    }
}
