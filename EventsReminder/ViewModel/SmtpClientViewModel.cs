using EventsReminder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                if (smtpClient.EmailSentSuccessful)
                {
                    return " Email has been sent successfully ";
                }
                else
                {
                    return " Send Email For Test ";
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
        public bool SendEmailButton_Click()
        {
            string result = "";// SendEmail.Send(this.SmtpClient, "morteza88@live.com", "TestEmail", "This Email send by WpfAAppSample1 for test.");
            if (result == "Sent Successfully")
            {
                EmailSentSuccessful = true;
            }
            EmailSentSuccessful = false;
            //MessageBox.Show(result, "Error");
            return false;
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
