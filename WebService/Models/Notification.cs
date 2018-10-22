using System;

namespace WebService.Models
{
    public class Notification
    {
        public Notification(string notification_id, DateTime received_date, DateTime read_date, string type, string target_user, bool status_read, string message)
        {
            this.notification_id = notification_id;
            this.received_date = received_date;
            this.read_date = read_date;
            this.type = type;
            this.target_user = target_user;
            this.status_read = status_read;
            this.message = message;
        }

        string notification_id { get; set; }
        DateTime received_date { get; set; }
        DateTime read_date { get; set; }
        string type { get; set; }
        string target_user { get; set; }
        bool status_read { get; set; }
        string message { get; set; }
    }
}