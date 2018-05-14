using EventsReminder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EventsReminder.Utility
{
    public class SendEmail
    {
        static public string Send(SmtpClientModel smtpClient, string toAddress, string subject, string body)
        {
            try
            {
                SmtpClient smtp = new SmtpClient
                {
                    Host = smtpClient.Host,
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential(smtpClient.Email, smtpClient.Password),
                    Timeout = 30000,
                };
                MailMessage message = new MailMessage(smtpClient.Email, toAddress, subject, body);
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Sent Successfully";
        }
    }
}
