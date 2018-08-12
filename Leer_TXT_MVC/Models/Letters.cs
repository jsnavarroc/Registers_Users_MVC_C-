using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leer_TXT_MVC.Models
{
    public class Letters
    {

        public string letter { get; set; }

        public int check { get; set; }

        public Letters(string letter, int check)
        {
            this.letter = letter;
            this.check = check;
        }
    }
}