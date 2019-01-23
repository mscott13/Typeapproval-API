using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class Certificate
    {
        public Certificate()
        {
            manufacturer_address = "";
            manufacturer_name = "";
            product_identification = "";
            equipment_description = "";
            remarks = "";
        }

        public List<Frequency> frequencies { get; set; }
        public string manufacturer_name { get; set; }
        public string manufacturer_address { get; set; }
        public string product_identification { get; set; }
        public string equipment_description { get; set; }
        public string remarks { get; set; }
    }
}