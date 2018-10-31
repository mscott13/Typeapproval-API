using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class Company
    {
        public string name { get; set; }
        public string telephone { get; set; }
        public string address { get; set; }
        public string fax { get; set; }
        public string cityTown { get; set; }
        public string contactPerson { get; set; }
        public string nationality { get; set; }
    }
}