using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class Form
    {
        public Form()
        {
            access_key = "";
            application_id = "";
            username = "";
            applicant_name = "";
            applicant_tel = "";
            applicant_address = "";
            applicant_fax = "";
            applicant_contact_person = "";
            manufacturer_name = "";
            grantee_address = "";
            manufacturer_name = "";
            equipment_type = "";
            equipment_description = "";
            product_identification = "";
            make = "";
            category = "";
            name_of_test = "";
            country = "";

            List<Frequency> frequencies = new List<Frequency>();
        }

        public string access_key { get; set; }
        public string application_id { get; set; }
        public string username { get; set; }
        public string applicant_name { get; set; }
        public string applicant_tel { get; set; }
        public string applicant_address { get; set; }
        public string applicant_fax { get; set; }
        public string applicant_contact_person { get; set; }
        public string grantee_name { get; set; }
        public string grantee_address { get; set; }
        public string equipment_type { get; set; }
        public string equipment_description { get; set; }
        public string product_identification { get; set; }
        public string make { get; set; }
       
        public List<Frequency> frequencies { get; set; }
        public string status { get; set; }
        public string category { get; set; }
        public string name_of_test { get; set; }
        public string country { get; set; }
        public string manufacturer_name { get; set; }
    }
}