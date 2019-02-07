using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class Frequency
    {
        public Frequency(string authorization_notes, string application_id, int sequence, string lower_freq, string upper_freq, string power, string tolerance, string emmission_desig)
        {
            this.sequence = sequence;
            this.lower_freq = lower_freq;
            this.upper_freq = upper_freq;
            this.power = power;
            this.tolerance = tolerance;
            this.emmission_desig = emmission_desig;
            this.application_id = application_id;
            this.authorization_notes = authorization_notes;
        }

        public string authorization_notes { get; set; }
        public string application_id { get; set; }
        public int sequence { get; set; }
        public string lower_freq { get; set; }
        public string upper_freq { get; set; }
        public string power { get; set; }
        public string tolerance { get; set; }
        public string emmission_desig { get; set; }
    }
}