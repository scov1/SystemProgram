using System;
using System.Threading;

namespace Lesson3
{
    class MainClass
    {

        public static void Main(string[] args)
        {

            Console.Write("Vvedite kolichestvo chisel: ");
            Thread thread = new Thread(new ParameterizedThreadStart(FibonaciParam));
            int inputNumber = Convert.ToInt32(Console.ReadLine());

  

          
             thread.Start(inputNumber);


            Console.WriteLine("Najmite klaviwu dlya ostanovki");
            Console.ReadKey();
            thread.Abort();
            Console.WriteLine("\nPotok ostanovlen");




            Console.ReadLine();
        }


        public static void FibonaciParam(object param)
        {

            int j = 1;
            int num = (int)param;

            if (num == 0)
            {
                Console.Write("Neverniy diapozon");
            }

            for (int i = 1; i <= num; i += j)
            {
                if (num == 1)
                {
                    Console.Write(1);
                }
                else
                {
                    Console.Write("{0} ", i);
                    j = i - j;
                }

                Thread.Sleep(1000);

            }
        }
    }
}
