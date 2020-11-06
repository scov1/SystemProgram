using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Groops
    {
        public string Name { get; set; }
        public List<Client> Clients { get; set; }

        public Client Admin { get; set; }

        public string Message { get; set; }

        public Client Sender { get; set; }

        public Groops(string name, Client admin)
        {
            Name = name;
            Admin = admin;
            Clients = new List<Client>();
        }
        public void AddGrpoop(Client client)
        {
            Clients.Add(client);
        }

        public void RemoveGroops(Client client)
        {
            Clients.Remove(client);
        }
    }
}
