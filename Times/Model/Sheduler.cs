using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Times.Model
{
    static class Sheduler
    {
        public static List<Period> Periods { get; set; }

        static Sheduler()
        {
            Periods = new List<Period>();
        }


        //определение текущего периода
        // определить какой сейчас   определить какой след 
        public static Period GetCurrentPeriod(DateTime current)
        {
            Time currentT = new Time(current.Hour, current.Minute, current.Second);

            Period periodResult = new Period("");

            foreach (var period in Periods)
            {
                if ((period.Begin - currentT).ToString().Contains("-"))
                {
                    periodResult = period;
                }
            }

            return periodResult;
        }

        public static Period GetNextPeriod(DateTime current)
        {
            Time currentT = new Time(current.Hour, current.Minute, current.Second);

            Period periodResult = new Period("");

            foreach (var period in Periods)
            {
                if (!(period.Begin - currentT).ToString().Contains("-"))
                {
                    return period;
                }
            }

            return periodResult;
        }

    }
}
