using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AriaView.Model
{
    public class AriaViewDateTerm
    {

        private String startDate, endDate, imgName,rawName;

        public String StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public String EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        public String ImgName
        {
            get { return imgName; }
            set { imgName = value; }
        }

        public String RawName
        {
            get { return rawName; }
            set { rawName = value; }
        }

       
        public AriaViewDateTerm(string rawName,string startDate,string endDate,string imgName)
        {
            RawName = rawName;
            StartDate = startDate;
            EndDate = endDate;
            ImgName = imgName;
        }


        public override string ToString()
        {
            return RawName;
        }
    }
}
