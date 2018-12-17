using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class LicensedApplication
    {
        public LicensedApplication(string application_id, string manufacturer, string model, string created_date, string licensed_date, string author, string category)
        {
            this.application_id = application_id;
            this.manufacturer = manufacturer;
            this.model = model;
            this.created_date = created_date;
            this.licensed_date = licensed_date;
            this.author = author;
            this.category = category;
        }

        public string application_id { get; set; }
        public string manufacturer { get; set; }
        public string model { get; set; }
        public string created_date { get; set; }
        public string licensed_date { get; set; }
        public string author { get; set; }
        public string category { get; set; }
    }
}