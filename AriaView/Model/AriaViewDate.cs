using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AriaView.Model
{
    public class AriaViewDate : INotifyPropertyChanged
    {
        private Double north,east,south,west;
        private List<AriaViewDateTerm> dateTerms;
        private List<Site> sites;
        private List<string> dates;
        private int currentTermIndex;
        private List<Pollutant> pollutantsList;
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
        public AriaViewDateTerm CurrentTerm
        {
            get
            {
                return DateTerms[currentTermIndex];
            }
        }
        public List<Site> Sites
        {
            get
            {
                return sites;
            }

            set
            {
                sites = value;
            }
        }
        public List<string> Dates
        {
            get
            {
                return dates;
            }

            set
            {
                dates = value;
            }
        }
        public List<AriaViewDateTerm> DateTerms
        {
            get { return dateTerms; }
            set 
            { 
                dateTerms = value;
                Notify("DateTerms");
            }
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
        public List<Pollutant> PollutantsList
        {
            get
            {
                return pollutantsList;
            }

            set
            {
                pollutantsList = value;
            }
        }

        public AriaViewDate(Double north,Double east,Double south,Double west
            ,List<AriaViewDateTerm> dateTermsList,
            List<Site> sitesList
            ,List<String> datesList)
        {
            dateTerms = dateTermsList;
            Sites = sitesList;
            Dates = datesList;
            North = north;
            East = east;
            South = south;
            West = west;
            currentTermIndex = 0;
        }


        public void Notify(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
