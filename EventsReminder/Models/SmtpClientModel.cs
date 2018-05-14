using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsReminder.Models
{
    public class SmtpClientModel
    {
        public int Id { get; set; }
        public string Host { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool EmailSentSuccessful { get; set; }
        public bool IsDefualt { get; set; }
        public SmtpClientModel() { }
        public SmtpClientModel(string host, string email, string password, bool emailSentSuccessful)
        {
            Host = host;
            Email = email;
            Password = password;
            EmailSentSuccessful = emailSentSuccessful;
        }
    }
}
