using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class FileSend
    {
        public Client Sender { get; set; }

        public Client Recipient { get; set; }
        public string Name { get; set; }

        public string Expansion { get; set; }

        public string Type { get; set; }
        public int ThisBlock { get; set; }

        public int AllBlocks { get; set; }

        public byte[] Data { get; set; }

        public FileSend(Client sender, Client recipient, string name, string expansion, string type, int thisBlock, int allBlocks, byte[] data)
        {
            Sender = sender;
            Recipient = recipient;
            Name = name;
            Expansion = expansion;
            Type = type;
            ThisBlock = thisBlock;
            AllBlocks = allBlocks;
            Data = data;
        }
    }
}


