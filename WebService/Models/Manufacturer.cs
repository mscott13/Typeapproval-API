using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class Grantee
    {
        public Grantee(string name, string address)
        {
            this.name = name;
            this.address = address;
        }

        public Grantee() { }

        public string name { get; set; }
        public string address { get; set; }
    }
}