using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    public class LogOnForm
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public string IpAddress { get; set; }
        public LogOnForm() { }
        public LogOnForm(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public void LoadJson(string json)
        {
            LogOnForm logOnForm = JsonConvert.DeserializeObject<LogOnForm>(json);
            Name = logOnForm.Name;
            Password = logOnForm.Password;
        }
    }
}
