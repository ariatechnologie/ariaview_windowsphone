using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AriaView.Model
{
    public class User
    {
        public String Id { get; set; }
        public String Login { get; set; }
        public String Password { get; set; }
        public List<Site> Sites {get; set;}

        public User()
        {
            Sites = new List<Site>();
        }
    }
}
