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
        public bool EmailSentSuccessful { get; set; }
        public DateTime StartDateTime { get; set; }
        public string Description { get; set; }

        public EventModel() { }     //An empty constructor is needed to create a new instance of LiteDatabase.
        public EventModel(string subject, string emailToRecall, bool emailSentSuccessful, DateTime startDateTime, string description)
        {
            Subject = subject;
            EmailToRecall = emailToRecall;
            EmailSentSuccessful = emailSentSuccessful;
            StartDateTime = startDateTime;
            Description = description;
        }
    }
}
