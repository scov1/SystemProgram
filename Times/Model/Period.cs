using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Times.Model
{
    class Period
    {
        public string Name { get; set; }
        public Time Begin { get; set; }

        public bool IsD1 { get; set; }
        public bool IsD2 { get; set; }
        public bool IsD3 { get; set; }
        public bool IsD4 { get; set; }
        public bool IsD5 { get; set; }
        public bool IsD6 { get; set; }
        public bool IsD7 { get; set; }

        


        public Period()
        { }

        public Period(string name) : this(name, new Time(), false, false, false, false, false, false, false)
        {
        }

        public Period(string name, Time begin) : this(name, begin, false, false, false, false, false, false, false)
        {
        }

        public Period(string name, Time begin, bool isD1) : this(name, begin, isD1, false, false, false, false, false, false)
        {
        }

        public Period(string name, Time begin, bool isD1, bool isD2) : this(name, begin, isD1, isD2, false, false, false, false, false)
        {
        }
        public Period(string name, Time begin, bool isD1, bool isD2, bool isD3) : this(name, begin, isD1, isD2, isD3, false, false, false, false)
        {
        }

        public Period(string name, Time begin, bool isD1, bool isD2, bool isD3, bool isD4) : this(name, begin, isD1, isD2, isD3, isD4, false, false, false)
        {
        }

        public Period(string name, Time begin, bool isD1, bool isD2, bool isD3, bool isD4, bool isD5, bool isD6, bool isD7)
        {
            Name = name;
            Begin = begin;
            IsD1 = isD1;
            IsD2 = isD2;
            IsD3 = isD3;
            IsD4 = isD4;
            IsD5 = isD5;
            IsD6 = isD6;
            IsD7 = isD7;
        }



        public override string ToString()
        {
            return $"{Name} ({Begin.ToString()}) ({WeekDayToString()})";
        }

        public string WeekDayToString()
        {

            List<string> days = new List<string>();

            if (IsD1)
                days.Add("Mon.");
            if (IsD2)
                days.Add("Tues.");
            if (IsD3)
                days.Add("Wed.");
            if (IsD4)
                days.Add("Thurs.");
            if (IsD5)
                days.Add("Fri.");
            if (IsD6)
                days.Add("Sat.");
            if (IsD7)
                days.Add("Sun.");

            return string.Join(", ", days);
        }
    }
}
