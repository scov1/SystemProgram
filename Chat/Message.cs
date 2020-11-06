using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    public class Message
    {
        public Client Sender { get; set; }
        public Client Recipient { get; set; }

        public string MessageText { get; set; }

        public DateTime Date { get; set; }

        public Message(Client sender, Client recipient, string messageText, DateTime date)
        {
            Sender = sender;
            Recipient = recipient;
            MessageText = messageText;
            Date = date;
        }
    }
}
