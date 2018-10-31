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
            this.telephone = telephone;
            this.address = address;
            this.fax = fax;
            this.cityTown = cityTown;
            this.contactPerson = contactPerson;
            this.nationality = nationality;
        }

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