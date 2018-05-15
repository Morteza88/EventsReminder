using EventsReminder.Models;
using EventsReminder.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static EventsReminder.ViewModel.SmtpClientViewModel;

namespace EventsReminder.Utility
{
    public class SendEmail
    {
        public delegate void Smtp_SendCompleted(object sender, AsyncCompletedEventArgs e);
        public delegate void Smtp_SendCompleted_WhitEventView(object sender, AsyncCompletedEventArgs e, EventViewModel eventViewModel);
        private Smtp_SendCompleted_WhitEventView smtp_SendCompleted_WhitEvent;
        private EventViewModel eventView;
        public string SendAsync(string host, string fromAddress, string password, string toAddress, string subject, string body, Smtp_SendCompleted_WhitEventView smtp_SendCompleted_WhitEventView, EventViewModel eventViewModel)
        {
            smtp_SendCompleted_WhitEvent = smtp_SendCompleted_WhitEventView;
            eventView = eventViewModel;
            return SendAsync(host, fromAddress, password, toAddress, subject, body, smtp_SendCompleted);
        }
        public string SendAsync(string host, string fromAddress, string password, string toAddress, string subject, string body, Smtp_SendCompleted smtp_SendCompleted)
        {
            try
            {
                SmtpClient smtp = new SmtpClient
                {
                    Host = host,
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential(fromAddress, password),
                    Timeout = 30000,
                };
                MailMessage message = new MailMessage(fromAddress, toAddress, subject, body);
                smtp.SendCompleted += new SendCompletedEventHandler(smtp_SendCompleted);
                smtp.SendAsync(message, message);
                return "Sending";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private void smtp_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            smtp_SendCompleted_WhitEvent(sender, e, eventView);
        }

        //public static string Send(string host, string fromAddress, string password, string toAddress, string subject, string body)
        //{
        //    try
        //    {
        //        SmtpClient smtp = new SmtpClient
        //        {
        //            Host = host,
        //            Port = 587,
        //            EnableSsl = true,
        //            DeliveryMethod = SmtpDeliveryMethod.Network,
        //            Credentials = new System.Net.NetworkCredential(fromAddress, password),
        //            Timeout = 30000,
        //        };
        //        MailMessage message = new MailMessage(fromAddress, toAddress, subject, body);
        //        smtp.Send(message);
        //        return "Sent Successfully";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
    }
}
