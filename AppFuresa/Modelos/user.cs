using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace AppFuresa.Modelos
{
    public class user
    {
        [PrimaryKey]
        public string Username { get; set; }
        public string Password { get; set; }
        public int NivelUser { get; set; }
    }
}
