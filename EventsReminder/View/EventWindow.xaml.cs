using EventsReminder.Models;
using EventsReminder.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EventsReminder.View
{
    /// <summary>
    /// Interaction logic for EventWindow.xaml
    /// </summary>
    public partial class EventWindow : Window
    {
        private EventModel currentEventModel { get; set; }
        public EventWindow(EventModel eventModel)
        {
            InitializeComponent();
            if (eventModel != null)
            {
                currentEventModel = new EventModel(eventModel.Subject, eventModel.EmailToRecall, eventModel.EmailSentSuccessful, eventModel.StartDateTime,eventModel.Description);
            }
            else
            {
                currentEventModel = new EventModel("", "", false, DateTime.Now, "");
            }
            this.EventView = new EventViewModel(currentEventModel);
        }
        public EventViewModel EventView
        {
            get { return (EventViewModel)GetValue(EventViewProperty); }
            set { SetValue(EventViewProperty, value); }
        }
        public static DependencyProperty EventViewProperty = DependencyProperty.Register("EventView", typeof(EventViewModel), typeof(EventWindow), null);
        public bool? ShowDialog(out EventModel newEventModel)
        {
            DialogResult = false;
            ShowDialog();
            newEventModel = currentEventModel;
            return DialogResult;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
