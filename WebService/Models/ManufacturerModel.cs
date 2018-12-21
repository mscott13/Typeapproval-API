using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class ManufacturerModel
    {
        public ManufacturerModel(string application_id,string manufacturer, string model, string status)
        {
            this.application_id = application_id;
            this.manufacturer = manufacturer;
            this.model = model;
            this.status = status;
        }

        public string application_id { get; set; }
        public string manufacturer { get; set; }
        public string model { get; set; }
        public string status { get; set; }
    }
}