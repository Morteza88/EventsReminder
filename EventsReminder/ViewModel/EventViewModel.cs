using EventsReminder.Models;
using EventsReminder.Utility;
using EventsReminder.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace EventsReminder.ViewModel
{
    public class EventViewModel : BaseViewModel
    {
        private EventModel eventModel;
        public EventModel EventModel
        {
            get { return eventModel; }
            set { eventModel = value; }
        }

        private object Parent = null;
        public EventViewModel Self { get { return this; } }
        public string Subject { get { return eventModel.Subject; } set { eventModel.Subject = value; } }
        public DateTime Date
        {
            get { return eventModel.StartDateTime; }
            set
            {
                eventModel.StartDateTime = new DateTime(value.Year, value.Month, value.Day, eventModel.StartDateTime.Hour, eventModel.StartDateTime.Minute, eventModel.StartDateTime.Second);
            }
        }
        public string Time
        {
            get { return eventModel.StartDateTime.ToShortTimeString(); }
            set
            {
                var time = DateTime.Parse(value);
                eventModel.StartDateTime = new DateTime(eventModel.StartDateTime.Year, eventModel.StartDateTime.Month, eventModel.StartDateTime.Day, time.Hour, time.Minute, time.Second);
            }
        }
        public string TimetoStart
        {
            get
            {
                TimeSpan timetoStart = eventModel.StartDateTime.Subtract(DateTime.Now);
                if (timetoStart.Ticks < 0)
                {
                    return "Past";
                }
                if (timetoStart.Days > 0)
                {
                    return timetoStart.Days + (timetoStart.Days == 1 ? " Day" : " Days") + "  " + timetoStart.Hours + (timetoStart.Hours == 1 ? " Hour" : " Hours");
                }
                if (timetoStart.Hours > 0)
                {
                    return timetoStart.Hours + (timetoStart.Hours == 1 ? " Hour" : " Hours") + "   " + timetoStart.Minutes + (timetoStart.Minutes == 1 ? " Minute" : " Minutes");
                }
                if (timetoStart.Minutes > 0)
                {
                    return timetoStart.Minutes + (timetoStart.Minutes == 1 ? " Minute" : " Minutes") + "   " + timetoStart.Seconds + (timetoStart.Seconds == 1 ? " Second" : " Seconds");
                }
                return timetoStart.Seconds + (timetoStart.Seconds == 1 ? " Second" : " Seconds");
            }
        }
        public bool Less1hourToStart
        {
            get
            {
                TimeSpan timetoStart = eventModel.StartDateTime.Subtract(DateTime.Now);
                if ((timetoStart.Ticks < 0) || (timetoStart.Days > 0) || (timetoStart.Hours > 0))
                {
                    return false;
                }
                return true;
            }
        }
        public string Email { get { return eventModel.EmailToRecall; } set { eventModel.EmailToRecall = value; } }
        public string Description { get { return eventModel.Description; } set { eventModel.Description = value; } }
        public bool EmailSentSuccessful { get { return eventModel.EmailSentSuccessful; } set { eventModel.EmailSentSuccessful = value; } }
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
                TimeSpan timetoStart = eventModel.StartDateTime.Subtract(DateTime.Now);
                if (timetoStart.Ticks < 0)
                {
                    if (eventModel.EmailSentSuccessful)
                    {
                        return new SolidColorBrush(Colors.WhiteSmoke);
                    }
                    else
                    {
                        return new SolidColorBrush(Color.FromRgb(170, 130, 130));
                    }
                }
                if ((timetoStart.Days > 0) || (timetoStart.Hours > 0))
                {
                    if (eventModel.EmailSentSuccessful)
                    {
                        return new SolidColorBrush(Colors.Yellow);
                    }
                    else
                    {
                        return new SolidColorBrush(Colors.White);
                    }
                }
                if (eventModel.EmailSentSuccessful)
                {
                    return new SolidColorBrush(Colors.LightGreen);
                }
                else
                {
                    return new SolidColorBrush(Colors.LightPink);
                }
            }
        }

        public EventViewModel(EventModel eventModel, object parent = null)
        {
            this.eventModel = eventModel;
            this.Parent = parent;
        }
        public void Refresh()
        {
            RaisePropertyChanged("Subject");
            RaisePropertyChanged("Date");
            RaisePropertyChanged("Time");
            RaisePropertyChanged("TimetoStart");
            RaisePropertyChanged("BackgroundColor");
            RaisePropertyChanged("Email");
            RaisePropertyChanged("Description");
            RaisePropertyChanged("EmailSentSuccessful");
        }
        internal void RefreshTimeToStart()
        {
            RaisePropertyChanged("TimetoStart");
            RaisePropertyChanged("BackgroundColor");
        }
        public bool EditButton_Click()
        {
            EventModel newModel;
            EventWindow editEventWindow = new EventWindow(eventModel);
            if (editEventWindow.ShowDialog(out newModel)==true)
            {
                eventModel.Subject = newModel.Subject;
                eventModel.EmailToRecall = newModel.EmailToRecall;
                eventModel.StartDateTime = newModel.StartDateTime;
                eventModel.Description = newModel.Description;
                eventModel.EmailSentSuccessful = newModel.EmailSentSuccessful;
                Refresh();
                EventDB.update(eventModel);
                Console.WriteLine("eventModel (Subject = "+eventModel.Subject+") updated successfully");
                return true;
            }
            return false;
        }
        public bool DeleteButton_Click()
        {
            if (MessageBox.Show("Are you sure to delete this event?", "Delete Event", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                EventDB.delete(eventModel);
                if (Parent != null) ((EventsReminderViewModel)Parent).Refresh(EventDB.selectAll());
                Console.WriteLine("eventModel (Subject = " + eventModel.Subject + ") delete successfully");
                return true;
            }
            return false;
        }
    }
}
