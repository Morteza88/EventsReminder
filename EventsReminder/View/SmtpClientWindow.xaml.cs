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
    /// Interaction logic for SmtpClientWindow.xaml
    /// </summary>
    public partial class SmtpClientWindow : Window
    {
        private SmtpClientModel currentSmtpClientModel { get; set; }
        public SmtpClientWindow()
        {
            InitializeComponent();
        }
        public SmtpClientViewModel SmtpClienView
        {
            get { return (SmtpClientViewModel)GetValue(EventViewProperty); }
            set { SetValue(EventViewProperty, value); }
        }
        public static DependencyProperty EventViewProperty = DependencyProperty.Register("SmtpClienView", typeof(SmtpClientViewModel), typeof(SmtpClientWindow), null);
        
        public bool ShowDialog(out SmtpClientModel newSmtpClientModel)
        {
            //DialogResult ss = new System.Windows.Forms.()
            ShowDialog();
            newSmtpClientModel = currentSmtpClientModel;
            return true;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //dialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            //dialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }
    }
}
