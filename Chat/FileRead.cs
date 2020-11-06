using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    public class FileRead
    {
        public Client Sender { get; set; }

        public Client Recipient { get; set; }
        public string Name { get; set; }

        public string Expansion { get; set; }

        public string Type { get; set; }
        public byte[] Data { get; set; }

        public FileRead(Client sender, Client recipient, string name,string expansion, string type)
        {
            Sender = sender;
            Recipient = recipient;
            Name = name;
            Expansion = expansion;
            Type = type;
            Data = new byte[0];
        }

        public void UploadData(byte[] data)
        {
            byte[] newData = new byte[data.Length + Data.Length];
           
            Data.CopyTo(newData,0);
            data.CopyTo(newData,Data.Length);
            Data = new byte[newData.Length];
            Array.Copy(newData, Data, newData.Length);
        }

        public List<FileSend> GetListFileSend()
        {
            List<FileSend> lst = new List<FileSend>();

            int blocks = Data.Length / 8000;
            int lastPosition = 0;

            for(int i = 0; i < blocks; i++)
            {
                byte[] newData = new byte[8000];
                Array.Copy(Data, lastPosition, newData, 0, 8000);
                FileSend fs;
                if (i + 1 != blocks)
                {
                     fs = new FileSend(Sender, Recipient, Name, Expansion, Type, i + 1, blocks, newData);
                }
                else
                {
                     fs = new FileSend(Sender, Recipient, Name, Expansion, Type, i + 1, blocks, Data.Skip(lastPosition).ToArray());
                }
                lst.Add(fs);
                lastPosition += 8000;
            }

            return lst;
        }
    }
}
