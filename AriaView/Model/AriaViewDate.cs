using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AriaView.Model
{
    public class AriaViewDate
    {
        private Double north,east,south,west;
        private List<AriaViewDateTerm> dateTerms;

        public List<AriaViewDateTerm> DateTerms
        {
            get { return dateTerms; }
            set { dateTerms = value; }
        }
        public Double North
        {
            get
            {
                return north;
            }

            set
            {
                north = value;
            }
        }
        public Double East
        {
            get
            {
                return east;
            }

            set
            {
                east = value;
            }
        }
        public Double South
        {
            get
            {
                return south;
            }

            set
            {
                south = value;
            }
        }
        public Double West
        {
            get
            {
                return west;
            }

            set
            {
                west = value;
            }
        }

        public AriaViewDate(Double north,Double east,Double south,Double west,List<AriaViewDateTerm> dateTermsList)
        {
            dateTerms = dateTermsList;
            North = north;
            East = east;
            South = south;
            West = west;
        }

        
    }
}
