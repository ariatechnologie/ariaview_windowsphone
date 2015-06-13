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
        public List<AriaViewDateTerm> DateTerms { get; set; }


        public Pollutant(String name, List<AriaViewDateTerm> dateTerms)
        {
            Name = name;
            DateTerms = dateTerms;
        }
    }
}
