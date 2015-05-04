using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using AriaView.Common;

namespace AriaView.ViewModel
{
    public class ViewModelBase : ObservableDictionary ,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged; 

        public void Notify(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
