using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Times.Model
{
    class Time
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        public Time()
        { }

        public Time(int h, int m, int s)
        {
            Hours = h;
            Minutes = m;
            Seconds = s;
        }

        public static Time operator -(Time t1, Time t2)
        {

            Time result = new Time(t1.Hours - t2.Hours, t1.Minutes - t2.Minutes, t1.Seconds - t2.Seconds);

            if (result.Seconds < 0)
            { 
                result.Minutes--;
                result.Seconds += 60;
            }

            if (result.Minutes < 0)
            {
                result.Hours--;
                result.Minutes += 60;
            }

            return result;
        }

        public static Time operator +(Time t1, Time t2)
        {

            Time result = new Time(t1.Hours + t2.Hours, t1.Minutes + t2.Minutes, t1.Seconds + t2.Seconds);

            if (result.Seconds >= 60)
            {
                result.Minutes++;
                result.Seconds -= 60;
            }

            if (result.Minutes >= 60)
            {
                result.Hours++;
                result.Minutes -= 60;
            }

            if (result.Hours >= 24)
                result.Hours -= 24;

            return result;
        }

        public override string ToString()
        {
            return $"{Hours.ToString("D2")}:{Minutes.ToString("D2")}:{Seconds.ToString("D2")}";
        }
    }
}
