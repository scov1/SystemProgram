using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    public class Client
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string IpAddress { get; set; }

        public bool IsConnect { get; set; }
        public Client(string name, string password, string ipAddress)
        {
            Name = name;
            Password = password;
            IpAddress = ipAddress;
        }
    }
}
