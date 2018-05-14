using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsReminder.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public void RaisePropertyChanged(string PropertyName)
        {
            if (PropertyName != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
