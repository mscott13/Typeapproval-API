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
            applicant_city_town = "";
            applicant_contact_person = "";
            applicant_nationality = "";
            manufacturer_name = "";
            manufacturer_tel = "";
            manufacturer_address = "";
            manufacturer_fax = "";
            manufacturer_contact_person = "";
            equipment_type = "";
            equipment_description = "";
            product_identification = "";
            refNum = "";
            make = "";
            software = "";
            type_of_equipment = "";
            other = "";
            antenna_type = "";
            antenna_gain = "";
            channel = "";
            separation = "";
            category = "";
            additional_info = "";
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
        public string applicant_city_town { get; set; }
        public string applicant_contact_person { get; set; }
        public string applicant_nationality { get; set; }
        public string manufacturer_name { get; set; }
        public string manufacturer_tel { get; set; }
        public string manufacturer_address { get; set; }
        public string manufacturer_fax { get; set; }
        public string manufacturer_contact_person { get; set; }
        public string equipment_type { get; set; }
        public string equipment_description { get; set; }
        public string product_identification { get; set; }
        public string refNum { get; set; }
        public string make { get; set; }
        public string software { get; set; }
        public string type_of_equipment { get; set; }
        public string other { get; set; }
        public string antenna_type { get; set; }
        public string antenna_gain { get; set; }
        public string channel { get; set; }
        public string separation { get; set; }
        public string additional_info { get; set; }
        public List<Frequency> frequencies { get; set; }
        public string status { get; set; }
        public string category { get; set; }
        public string name_of_test { get; set; }
        public string country { get; set; }
    }
}