using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsReminder.Models
{
    public class EventModel
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string EmailToRecall { get; set; }
        public bool EmailSentSeccessful { get; set; }
        public DateTime StartDateTime { get; set; }
        public string Description { get; set; }

        public EventModel(string subject, string emailToRecall, bool emailSentSeccessful, DateTime startDateTime, string description)
        {
            Subject = subject;
            EmailToRecall = emailToRecall;
            EmailSentSeccessful = emailSentSeccessful;
            StartDateTime = startDateTime;
            Description = description;
        }
    }
}
