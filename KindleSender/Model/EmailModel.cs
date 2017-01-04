using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindleSender
{
    public class EmailModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string From { get; set; }

        public string Password { get; set; }

        public string Smtp { get; set; }

        public int Port { get; set; }

        public string To { get; set; }
    }
}
