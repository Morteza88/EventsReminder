﻿using EventsReminder.Models;
using EventsReminder.Utility;
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
    /// Interaction logic for EventsReminderWindow.xaml
    /// </summary>
    public partial class EventsReminderWindow : Window
    {
        public EventsReminderWindow()
        {
            InitializeComponent();

            TextBoxOutputter outputter;
            InitializeComponent();
            outputter = new TextBoxOutputter(TestBox);
            Console.SetOut(outputter);
            Console.WriteLine("EventsReminder app started");

            EventDB.init();
            IEnumerable<EventModel> eventModels = EventDB.selectAll();
            Console.WriteLine("Events (Count="+ eventModels.ToList().Count + ") loaded from LiteDB");

            this.EventsReminderView = new EventsReminderViewModel(eventModels);
        }
        public EventsReminderViewModel EventsReminderView
        {
            get { return (EventsReminderViewModel)GetValue(EventsReminderViewProperty); }
            set { SetValue(EventsReminderViewProperty, value); }
        }
        public static DependencyProperty EventsReminderViewProperty = DependencyProperty.Register("EventsReminderView", typeof(EventsReminderViewModel), typeof(EventsReminderWindow), null);

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TestBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TestBox.ScrollToEnd();
        }
    }
}
