using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Times.Model;

namespace Times
{
    class Program
    {
        static void Main(string[] args)
        {
            var t1 = new Time(10, 1, 10);
            var t2 = new Time(11, 0, 0);
            var t3 = new Time(11, 10, 10);
            var t4 = new Time(11, 20, 0);
            var t5 = new Time(12, 00, 00);
            var t6 = new Time(12, 00, 7);

            var tLesson1Start = new Time(18, 30, 0);
            var tBreakStart = new Time(19, 50, 0);
            var tLesson2Start = new Time(20, 00, 0);
            var tLesson2End = new Time(21, 20, 0);

            /*Console.WriteLine(t1);
            Console.WriteLine(t2);
            Console.WriteLine(t3);
            Console.WriteLine(t4);
            */
            Console.WriteLine("Test1: " + (t4 + t1));
            Console.WriteLine("Test2: " + (t5 + t1));
            Console.WriteLine("Test3: " + (t6 + t1));
            Console.WriteLine("Test4: " + (t1 + t6));
            //Console.WriteLine("Test5: " + (t7 - t1));



            Sheduler.Periods.Add(new Period("L1", tLesson1Start, true, true, true, true, false, false, false));
            Sheduler.Periods.Add(new Period("Break", tBreakStart, true, true, true, true, false, false, false));
            Sheduler.Periods.Add(new Period("L2", tLesson2Start, true, true, true, true, false, false, false));
            Sheduler.Periods.Add(new Period("The End", tLesson2End, true, true, true, true, false, false, false));

            /*foreach (var p in Sheduler.Periods)
                Console.WriteLine(p);
            */
            Console.WriteLine("=================");

            DateTime dt = new DateTime(2000, 1, 1, 0, 0, 0);
            for (int i = 0; i < 1440; i++)
            {
                Console.WriteLine("Current Time: " + dt);
                dt = dt.AddMinutes(1);
                var current = Sheduler.GetCurrentPeriod(dt);
                var next = Sheduler.GetNextPeriod(dt);
                Console.WriteLine("Current Period: " + current);
                Console.WriteLine("Next Period: " + next);
            }
            Console.ReadKey();
        }
    }
}
