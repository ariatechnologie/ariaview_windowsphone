//Author Jérôme Cambray
//Version 1.0


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AriaView.Model
{
    public class ChartPoint
    {
        public double X { get; set; }
        public double Y { get; set; }

        public ChartPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

    }
}
