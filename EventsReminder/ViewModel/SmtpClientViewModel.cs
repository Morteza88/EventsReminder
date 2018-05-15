using EventsReminder.Models;
using EventsReminder.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace EventsReminder.ViewModel
{
    public class SmtpClientViewModel : BaseViewModel
    {
        private SmtpClientModel smtpClient;
        public SmtpClientModel SmtpClient
        {
            get { return smtpClient; }
            set { smtpClient = value; }
        }

        public string Host
        {
            get { return smtpClient.Host; }
            set
            {
                
                if (smtpClient.Host != value)
                {
                    smtpClient.EmailSentSuccessful = false;
                    smtpClient.Host = value;
                    Refresh();
                }
            }
        }
        public string Email
        {
            get { return smtpClient.Email; }
            set
            {
                if (smtpClient.Email != value)
                {
                    smtpClient.EmailSentSuccessful = false;
                    smtpClient.Email = value;
                    Refresh();
                }
            }
        }
        public string Password
        {
            get { return smtpClient.Password; }
            set
            {
                if (smtpClient.Password != value)
                {
                    smtpClient.EmailSentSuccessful = false;
                    smtpClient.Password = value;
                    Refresh();
                }
            }
        }
        public bool EmailSentSuccessful
        {
            get { return smtpClient.EmailSentSuccessful; }
            set
            {
                if (smtpClient.EmailSentSuccessful != value)
                {
                    smtpClient.EmailSentSuccessful = value;
                    Refresh();
                }
            }
        }

        private bool isSending = false;
        public bool IsSending
        {
            get { return isSending; }
            set { isSending = value; }
        }

        public SolidColorBrush BackgroundColor
        {
            get
            {
                if (smtpClient.EmailSentSuccessful)
                {
                    return new SolidColorBrush(Color.FromRgb(240, 255, 240));
                }
                else
                {
                    return new SolidColorBrush(Color.FromRgb(255, 255, 240));
                }
            }
        }
        public SolidColorBrush ButtonColor
        {
            get
            {
                if (smtpClient.EmailSentSuccessful)
                {
                    return new SolidColorBrush(Colors.LightGreen);
                }
                else
                {
                    return new SolidColorBrush(Color.FromRgb(240, 240, 180));
                }
            }
        }
        public string ButtonContent
        {
            get
            {
                if (isSending)
                {
                    return " Sending Email ... ";
                }
                else
                {
                    if (smtpClient.EmailSentSuccessful)
                    {
                        return " Email has been sent successfully ";
                    }
                    else
                    {
                        return " Send Email for test ";
                    }
                }
            }
        }
        public ICommand SendEmailButtonInCommand { get; set; }
        public SmtpClientViewModel(SmtpClientModel smtpClientModel)
        {
            this.smtpClient = smtpClientModel;
            SendEmailButtonInCommand = new SendEmailButtonClick();
        }
        public void Refresh()
        {
            RaisePropertyChanged("Host");
            RaisePropertyChanged("Email");
            RaisePropertyChanged("Password");
            RaisePropertyChanged("EmailSentSuccessful");
            RaisePropertyChanged("BackgroundColor");
            RaisePropertyChanged("ButtonColor");
            RaisePropertyChanged("ButtonContent");
        }
        public void SendEmailButton_Click()
        {
            SendEmail sendEmail = new SendEmail();
            string result = sendEmail.SendAsync(SmtpClient.Host, smtpClient.Email, smtpClient.Password, "morteza88@live.com", "TestEmail", "This Email send by WpfAAppSample1 for test.", smtp_SendCompleted);
            if (result == "Sending")
            {
                IsSending = true;
                Console.WriteLine("Sending Email ... ");
            }
            else
            {
                Console.WriteLine("Error sending email (Subject = " + "TestEmail" + "): " + result);
            }
            Refresh();
        }
        private void smtp_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                EmailSentSuccessful = false;
                Console.WriteLine("Error sending email (Subject = " + "TestEmail" + "): " + e.Error.ToString());
                //MessageBox.Show(e.Error.ToString(), "Error sending email", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                EmailSentSuccessful = true;
                Console.WriteLine("Email (Subject = " + "TestEmail" + ") has been sent");
            }
            IsSending = false;
            Refresh();
        }
    }
    public class SendEmailButtonClick : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            ((SmtpClientViewModel)parameter).SendEmailButton_Click();
        }
    }
}
