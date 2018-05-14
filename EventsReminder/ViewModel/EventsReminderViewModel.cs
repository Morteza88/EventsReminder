using EventsReminder.Models;
using EventsReminder.Utility;
using EventsReminder.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace EventsReminder.ViewModel
{
    public class EventsReminderViewModel : BaseViewModel
    {
        private SmtpClientModel smtpClient;
        public ObservableCollection<EventViewModel> Events { get; private set; }
        private bool ServiceStarted = false;
        public SolidColorBrush SmtpColor
        {
            get
            {
                if (smtpClient.EmailSentSuccessful)
                {
                    return new SolidColorBrush(Colors.LightGreen);
                }
                else
                {
                    return new SolidColorBrush(Colors.LightYellow);
                }
            }
        }
        public SolidColorBrush ServiceColor
        {
            get
            {
                if (ServiceStarted)
                {
                    return new SolidColorBrush(Colors.LightGreen);
                }
                else
                {
                    return new SolidColorBrush(Colors.LightYellow);
                }
            }
        }
        public string ServiceStatus
        {
            get
            {
                if (ServiceStarted)
                {
                    return "  Service is runnig . . .  ";
                }
                else
                {
                    return "  Service stoped.  ";
                }
            }
        }
        public string RunServerButtonContent
        {
            get
            {
                if (ServiceStarted)
                {
                    return " Stop Service ";
                }
                else
                {
                    return " Run Service ";
                }
            }
        }
        public ICommand EditButtonInCommand { get; set; }
        public ICommand DeleteButtonInCommand { get; set; }
        public ICommand AddButtonInCommand { get; set; }
        public ICommand EditSmtpClientButtonInCommand { get; set; }
        public ICommand ERunServiceButtonInCommand { get; set; }

        DispatcherTimer ServiceTimer = new DispatcherTimer();

        public EventsReminderViewModel(IEnumerable<EventModel> eventModels)
        {
            SmtpClientDB.init();
            smtpClient = SmtpClientDB.DefualtSmtpClientModel;
            Console.WriteLine("smtpClient loaded from LiteDB successfully");
            Events = new ObservableCollection<EventViewModel>();
            foreach (var item in eventModels)
            {
                Events.Add(new EventViewModel(item, this));
            }
            EditButtonInCommand = new EditButtonClick();
            DeleteButtonInCommand = new DeleteButtonClick();
            AddButtonInCommand = new AddButtonClick();
            EditSmtpClientButtonInCommand = new EditSmtpClientButtonClick();
            ERunServiceButtonInCommand = new RunServiceButtonClick();
            ServiceTimer.Tick += new EventHandler(ServiceTimer_Tick);
            ServiceTimer.Interval = new TimeSpan(0, 0, 1);
        }

        private void ServiceTimer_Tick(object sender, EventArgs e)
        {
            foreach (var item in Events)
            {
                if (item.Less1hourToStart && !item.EmailSentSuccessful)
                {
                    string body = "Hi \n This Email sent for reminding event.\nEvent Subject : " + item.Subject + " Event Start Date Time " + item.Date + "\nDescription : \n   " + item.Description;
                    string result = SendEmail.Send(SmtpClientDB.DefualtSmtpClientModel, item.Email, item.Subject, body);
                    if (result == "Sent Successfully")
                    {
                        item.EmailSentSuccessful = true;
                        EventDB.update(item.EventModel);
                        item.Refresh();
                        Console.WriteLine("Email has been sent successfully");
                    }
                    else
                    {
                        Console.WriteLine("Error sending email : " + result);
                    }
                }
                item.RefreshTimeToStart();
            }
        }
        public void Refresh(IEnumerable<EventModel> eventModels)
        {
            Events.Clear();
            foreach (var item in eventModels)
            {
                Events.Add(new EventViewModel(item, this));
            }
            RefreshColorsAndContents();
        }
        public void RefreshColorsAndContents()
        {
            RaisePropertyChanged("SmtpColor");
            RaisePropertyChanged("ServiceColor");
            RaisePropertyChanged("RunServerButtonContent");
            RaisePropertyChanged("ServiceStatus");
        }
        public bool AddButton_Click()
        {
            EventModel newModel;
            EventWindow addEventWindow = new EventWindow();
            if (addEventWindow.ShowDialog(out newModel) == true)
            {
                EventDB.insert(newModel);
                Console.WriteLine("newModel inserted to LiteDB successfully");
                return true;
            }
            return false;
        }
        public bool EditSmtpClientButton_Click()
        {
            SmtpClientModel newSmtpClientModel = new SmtpClientModel(smtpClient.Host, smtpClient.Email, smtpClient.Password, smtpClient.EmailSentSuccessful);
            SmtpClientWindow smtpClientWindow = new SmtpClientWindow(newSmtpClientModel);
            if (smtpClientWindow.ShowDialog(out newSmtpClientModel) == true)
            {
                smtpClient.Host = newSmtpClientModel.Host;
                smtpClient.Email = newSmtpClientModel.Email;
                smtpClient.Password = newSmtpClientModel.Password;
                smtpClient.EmailSentSuccessful = newSmtpClientModel.EmailSentSuccessful;
                smtpClient.IsDefualt = true;
                RefreshColorsAndContents();
                SmtpClientDB.update(smtpClient);
                Console.WriteLine("smtpClient update to LiteDB successfully");
                return true;
            }
            return false;
        }
        public void RunServiceButton_Click()
        {
            if (ServiceStarted)
            {
                ServiceTimer.Stop();
                ServiceStarted = false;
                Console.WriteLine("service stoped");
            }
            else if (smtpClient.EmailSentSuccessful)
            {
                ServiceTimer.Start();
                ServiceStarted = true;
                Console.WriteLine("service is runnig . . .");
            }
            else
            {
                MessageBox.Show( "Please edit your MstpClient.", "Warning",MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            RefreshColorsAndContents();
        }
    }
    public class EditButtonClick : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            ((EventViewModel)parameter).EditButton_Click();
        }
    }
    public class DeleteButtonClick : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            ((EventViewModel)parameter).DeleteButton_Click();
        }
    }
    public class AddButtonClick : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (((EventsReminderViewModel)parameter).AddButton_Click())
            {
                ((EventsReminderViewModel)parameter).Refresh(EventDB.selectAll());
            }
        }
    }
    public class EditSmtpClientButtonClick : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (((EventsReminderViewModel)parameter).EditSmtpClientButton_Click())
            {
                ((EventsReminderViewModel)parameter).Refresh(EventDB.selectAll());
            }
        }
    }
    public class RunServiceButtonClick : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            ((EventsReminderViewModel)parameter).RunServiceButton_Click();
        }
    }
}
