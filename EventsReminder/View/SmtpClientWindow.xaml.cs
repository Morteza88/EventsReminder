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
        public SmtpClientWindow(SmtpClientModel smtpClientModel = null)
        {
            InitializeComponent();
            if (smtpClientModel != null)
            {
                currentSmtpClientModel = new SmtpClientModel(smtpClientModel.Host, smtpClientModel.Email, smtpClientModel.Password, smtpClientModel.EmailSentSuccessful);
            }
            else
            {
                currentSmtpClientModel = new SmtpClientModel("","","",false);
            }
            this.SmtpClienView = new SmtpClientViewModel(currentSmtpClientModel);

        }
        public SmtpClientViewModel SmtpClienView
        {
            get { return (SmtpClientViewModel)GetValue(EventViewProperty); }
            set { SetValue(EventViewProperty, value); }
        }
        public static DependencyProperty EventViewProperty = DependencyProperty.Register("SmtpClienView", typeof(SmtpClientViewModel), typeof(SmtpClientWindow), null);
        
        public bool? ShowDialog(out SmtpClientModel newSmtpClientModel)
        {
            //DialogResult = false;
            ShowDialog();
            newSmtpClientModel = currentSmtpClientModel;
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
