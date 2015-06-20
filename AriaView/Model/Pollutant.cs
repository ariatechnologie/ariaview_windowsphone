using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AriaView.Model
{
    public class Pollutant
    {
        private int currentTermIndex;
        public int CurrentTermIndex
        {
            get
            {
                return currentTermIndex;
            }

            set
            {
                currentTermIndex = value;
            }
        }
        public String LegendImage { get; set; }
        public AriaViewDateTerm CurrentTerm
        {
            get
            {
                return DateTerms[currentTermIndex];
            }
        }
        public string Name { get; set; }
        public List<AriaViewDateTerm> DateTerms { get; set; }


        public Pollutant(String name, List<AriaViewDateTerm> dateTerms, string legendImage)
        {
            Name = name;
            DateTerms = dateTerms;
            currentTermIndex = 0;
            LegendImage = legendImage;
        }
    }
}
