using System;
using System.Threading;
using System.IO;

namespace HomeWorkWriteFile
{
    class MainClass
    {
        static object locker = new object();

        public static void Main(string[] args)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(WriteText));
            Console.WriteLine("Vvedite chislo: ");
            int num = Convert.ToInt32(Console.ReadLine());

            thread.Start(num);

            Console.WriteLine("Najmite klaviwu dlya ostanovki");
            Console.ReadKey();
            thread.Abort();
            Console.WriteLine("\nPotok ostanovlen");
        }


        static void WriteText(object number)
        {
            StreamWriter str = new StreamWriter("test.txt");
            int num = (int)number;
            lock (locker)
            {
                for (int i = 2; i < Int32.MaxValue; i++)
                {
                   
                    Random rand = new Random();
                    int random = rand.Next(0, 100);

                    if (random <= num)
                    {
                        continue;
                    }
                    if(IsPrimeNumber(random))
                    {
                        Console.WriteLine(random);
                        str.WriteLine(random);
                    }
                }
            }
            str.Close();
        }

        private static bool IsPrimeNumber(int number)
        {
            int sqrtNumber = (int)(Math.Sqrt(number));
            for (int i = 2; i <= sqrtNumber; i++)
            {
                if (number % i == 0)
                    return false;
            }
            return true;
        }
    }
}
