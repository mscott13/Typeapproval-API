using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class ClientCompany
    {
        public ClientCompany(string clientId, string name, string telephone, string address, string fax, string cityTown, string contactPerson, string nationality)
        {
            this.clientId = clientId;
            this.name = name;
            this.telephone =  (telephone=="") ? "" : telephone;
            this.address = (address == "") ? "" : address;
            this.fax = (fax == "") ? "" : fax;
            this.cityTown = (cityTown == "") ? "" : cityTown;
            this.contactPerson = (contactPerson == "") ? "" : contactPerson; 
            this.nationality = (nationality == "") ? "" : nationality;

            this.clientId = this.clientId.Trim();
            this.name = this.name.Trim();
            this.telephone = this.telephone.Trim();
            this.address = this.address.Trim();
            this.fax = this.fax.Trim();
            this.cityTown = this.cityTown.Trim();
            this.contactPerson = this.contactPerson.Trim();
            this.nationality = this.nationality.Trim();
        }

        public ClientCompany() { }

        public string clientId { get; set; }
        public string name { get; set; }
        public string telephone { get; set; }
        public string address { get; set; }
        public string fax { get; set; }
        public string cityTown { get; set; }
        public string contactPerson { get; set; }
        public string nationality { get; set; }
    }
}