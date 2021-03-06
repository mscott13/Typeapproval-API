﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class KeyDetail
    {
        public KeyDetail(int user_id, string username, string access_key, DateTime last_detected_activity, int max_inactivity_length, int user_role, string user_type)
        {
            this.user_id = user_id;
            this.access_key = access_key;
            this.last_detected_activity = last_detected_activity;
            this.max_inactivity_length = max_inactivity_length;
            this.username = username;
            this.user_role = user_role;
            this.user_type = user_type;
            data_present = true;
        }

        public KeyDetail()
        {
            user_id = -1;
            access_key = "";
            username = "";
            last_detected_activity = DateTime.Now;
            max_inactivity_length = -1;
            data_present = false;
            user_type = "";
            user_role = -1;
        }

        public int user_id { get; set; }
        public string username { get; set; }
        public string access_key { get; set; }
        public DateTime last_detected_activity { get; set; }
        public int max_inactivity_length { get; set; }
        public bool data_present { get; }
        public string user_type { get; set; }
        public int user_role { get; set; }
    }
}