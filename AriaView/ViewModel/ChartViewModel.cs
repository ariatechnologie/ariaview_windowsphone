using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AriaView.Model;

namespace AriaView.ViewModel
{
    public class ChartViewModel
    {
         public ObservableCollection<ChartPoint> Points { get; set; }
        public ChartViewModel()
        {
            Points = new ObservableCollection<ChartPoint>();
        }
    }
}
