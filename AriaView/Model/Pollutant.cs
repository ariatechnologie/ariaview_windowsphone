using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AriaView.Model
{
    public class Pollutant
    {
        public string Name { get; set; }
        public List<string> ImagesList { get; set; }


        public Pollutant(String name, List<string> imagesList)
        {
            Name = name;
            ImagesList = imagesList;
        }
    }
}
