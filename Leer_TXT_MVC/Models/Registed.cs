using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leer_TXT_MVC.Models
{
    public class Registed
    {
        public string name { get; set; }

        public int check { get; set; }

        public Registed(string name, int check)
        {
            this.name = name;
            this.check = check;
        }
    }
}