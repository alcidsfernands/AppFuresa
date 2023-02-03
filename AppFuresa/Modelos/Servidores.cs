using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace AppFuresa.Modelos
{
    public class Servidores
    {
        public string Name { get; set; }
        [PrimaryKey]
        public string IP { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Tipo{ get; set; }

    }
}
