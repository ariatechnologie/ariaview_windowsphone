using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AriaView.Model;
using Windows.Data.Xml.Dom;
using System.Xml;
using System.Xml.Linq;
using AriaView.Common;
using Windows.Storage;

namespace AriaView.ViewModel
{
   public class SiteSelectionViewModel : ObservableDictionary
   {
     

       public SiteSelectionViewModel()
       {   

       }

       /// <summary>
       /// Parses the xml response and stores the datas in the viewModel dictionnary
       /// </summary>
       /// <param name="xml"></param>
       public void ParseResponse(string xml)
       {
           var doc = XDocument.Parse(xml);

           var list = new List<String>();
           foreach (var s in doc.Descendants("sites").Descendants())
           {
               list.Add(s.Value);
           }

           foreach (var sitename in list)
           {
               var user = (User)this["user"];
               if(!user.Sites.Select(X => X.Name).Contains(sitename))
                    user.Sites.Add(new Site { Name = sitename });
           }
           ((User)this["user"]).Id = doc.Descendants("user").ElementAt(0).Attribute("id").Value;
       }
   }
}
