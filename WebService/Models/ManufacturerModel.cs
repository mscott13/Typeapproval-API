using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class ManufacturerModel
    {
        public ManufacturerModel(string manufacturer, string model)
        {
            this.manufacturer = manufacturer;
            this.model = model;
        }

        public string manufacturer { get; set; }
        public string model { get; set; }
    }
}