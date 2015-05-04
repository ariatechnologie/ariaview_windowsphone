﻿using System;
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
               ((User)this["user"]).Sites.Add(new Site { Name = sitename });
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
           var appTempFolder = await folder.CreateFolderAsync("ariaView",CreationCollisionOption.ReplaceExisting);
           var dateStorageFile = await appTempFolder.CreateFileAsync("dates");
           var datesXml = await ws.GetDatesAsync(url + (string)this["datefile"]);
           await FileIO.WriteTextAsync(dateStorageFile,datesXml);
           var mostRecentDate = XDocument.Parse(datesXml).Descendants("Folder").Descendants("name").ElementAt(0).Value;
           var kmlStorageFile = await appTempFolder.CreateFileAsync("kml");
           var kml = await ws.getKmlAsync(url + "/" + mostRecentDate + "/" + mostRecentDate + ".kml");
           await FileIO.WriteTextAsync(kmlStorageFile, kml);
           this["localdatefile"] = dateStorageFile;
           this["siteInfoUrl"] = url + "/" + mostRecentDate;
           this["localkmlfile"] = kmlStorageFile;
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
           return String.Format("{0}/{1}/{2}/GEARTH/{3}_{4}/", host, url, site, model, nest);
       }
   }
}
