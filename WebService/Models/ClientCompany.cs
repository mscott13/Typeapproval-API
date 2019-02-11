using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class ClientCompany
    {
        public ClientCompany(string clientId, string name, string telephone, string address, string fax, string contactPerson)
        {
            this.clientId = clientId;
            this.name = name;
            this.telephone =  (telephone=="") ? "" : telephone;
            this.address = (address == "") ? "" : address;
            this.fax = (fax == "") ? "" : fax;
            this.contactPerson = (contactPerson == "") ? "" : contactPerson; 

            this.clientId = this.clientId.Trim();
            this.name = this.name.Trim();
            this.telephone = this.telephone.Trim();
            this.address = this.address.Trim();
            this.fax = this.fax.Trim();
            this.contactPerson = this.contactPerson.Trim();
        }

        public ClientCompany(string user_type)
        {
            this.user_type = user_type;
        }

        public string clientId { get; set; }
        public string name { get; set; }
        public string telephone { get; set; }
        public string address { get; set; }
        public string fax { get; set; }
        public string contactPerson { get; set; }
        public string user_type { get; set; }
    }
}