using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class Frequency
    {
        public Frequency(string application_id, int sequence, string lower_freq, string upper_freq, string power, string tolerance, string emmission_desig, string freq_type)
        {
            this.sequence = sequence;
            this.lower_freq = lower_freq;
            this.upper_freq = upper_freq;
            this.power = power;
            this.tolerance = tolerance;
            this.emmission_desig = emmission_desig;
            this.freq_type = freq_type;
            this.application_id = application_id;
        }

        public string application_id { get; set; }
        public int sequence { get; set; }
        public string lower_freq { get; set; }
        public string upper_freq { get; set; }
        public string power { get; set; }
        public string tolerance { get; set; }
        public string emmission_desig { get; set; }
        public string freq_type { get; set; }
    }
}