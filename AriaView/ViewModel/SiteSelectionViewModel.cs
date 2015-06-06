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
           var list = doc.Descendants("sites").Select(X => X.Value);
           foreach (var sitename in list)
           {
               var user = (User)this["user"];
               if(!user.Sites.Select(X => X.Name).Contains(sitename))
                    user.Sites.Add(new Site { Name = sitename });
           }
           ((User)this["user"]).Id = doc.Descendants("user").ElementAt(0).Attribute("id").Value;
       }

       /// <summary>
       /// Get the datas to build the map and stores them in the temporary folder
       /// </summary>
       /// <param name="site"></param>
       /// <returns></returns>
       public async Task GetSiteInfoAsync(Site site)
       {
           var ws = new WebService.AriaViewWS();
           var url =  BuildUrl(await ws.GetSitesInfosAsync(site, (User)this["user"]));
           var folder = ApplicationData.Current.TemporaryFolder;

           var datesXml = await ws.GetDatesAsync(url + (string)this["datefile"]);
           this["datesXml"] = datesXml;
           var datesList = new List<String>();
           foreach (var date in XDocument.Parse(datesXml).Descendants("Folder").Descendants("name"))
               datesList.Add(date.Value);
           var mostRecentDate = datesList.Last();
           this["datesList"] = datesList;
           var kmlString = await ws.GetKmlAsync(url + "/" + mostRecentDate + "/" + mostRecentDate + ".kml");
           this["kmlString"] = kmlString;
           this["siteInfoUrl"] = url + "/" + mostRecentDate;
       }

       private String BuildUrl(string xml)
       {
           var doc = XDocument.Parse(xml);
           this["datefile"] = doc.Descendants("datefile").ElementAt(0).Value;
           var host = doc.Descendants("host").ElementAt(0).Value;
           var url = doc.Descendants("url").ElementAt(0).Value;
           var model = doc.Descendants("model").ElementAt(0).Value;
           var site = doc.Descendants("site").ElementAt(0).Value;
           var nest = doc.Descendants("nest").ElementAt(0).Value;
           var strBuilder = new StringBuilder(host);
           var urlParts = new Dictionary<string,string>();
           urlParts.Add("host", host);
           urlParts.Add("url", url);
           urlParts.Add("site", site);
           urlParts.Add("model", model);
           urlParts.Add("nest", nest);
           this["urlParts"] = urlParts;
           return String.Format("{0}/{1}/{2}/GEARTH/{3}_{4}/", host, url, site, model, nest);
       }
   }
}
