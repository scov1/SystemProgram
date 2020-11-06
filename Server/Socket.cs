using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Socket
    {
        public string Type { get; set; }
        public object Obj { get; set; }

        public Socket(string type, object obj)
        {
            Type = type;
            Obj = obj;
        }
    }
}
