using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Security.Credentials;
using AriaView.Common;
using Windows.Networking.Connectivity;
using AriaView.Model;
using System.Xml.Linq;

namespace AriaView.ViewModel
{
    public class AuthentificationViewModel : ObservableDictionary
    {
        private readonly String VAULT_RESOURCE = "AriaView Credentials";
        private PasswordVault vault = new PasswordVault();
     

        public AuthentificationViewModel()
        {
            Add("Login", "");
            Add("Password", "");
            var credentials = GetCredentials();
            if (credentials == null)
                return;
            this["Login"] = credentials.UserName;
            this["Password"] = credentials.Password;          
        }


        public async Task<String> AuthentificationAsync()
        {
            var ws = new AriaView.WebService.AriaViewWS();
             return await ws.Authentificate((string)this["Login"], (string)this["Password"]);
        }

        public void SaveCredentials()
        {
           
            if (vault.RetrieveAll().Count(X => X.Resource == VAULT_RESOURCE) > 0)
                RemoveCredentials();
            vault.Add(new PasswordCredential(VAULT_RESOURCE, (string)this["Login"], (string)this["Password"]));
        }


        public PasswordCredential GetCredentials()
        {
            try
            {
                var credentials = vault.FindAllByResource(VAULT_RESOURCE).ElementAt(0);
                credentials.RetrievePassword();
                return credentials;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void RemoveCredentials()
        {
            if (vault.RetrieveAll().Count == 0)
                return;
            var credential = GetCredentials();
            vault.Remove(credential);
        }

        public async Task GetSiteInfoAsync(Site site)
        {
            var ws = new WebService.AriaViewWS();
            var url = BuildUrl(await ws.GetSitesInfosAsync(site, (User)this["user"]));
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
            var urlParts = new Dictionary<string, string>();
            urlParts.Add("host", host);
            urlParts.Add("url", url);
            urlParts.Add("site", site);
            urlParts.Add("model", model);
            urlParts.Add("nest", nest);
            this["urlParts"] = urlParts;
            return String.Format("{0}/{1}/{2}/GEARTH/{3}_{4}/", host, url, site, model, nest);
        }

        public void ParseResponse(string xml)
        {
            var doc = XDocument.Parse(xml);
            var list = new List<String>();
            foreach(var s in doc.Descendants("sites").Descendants())
            {
                list.Add(s.Value);
            }

            foreach (var sitename in list)
            {
                var user = (User)this["user"];
                if (!user.Sites.Select(X => X.Name).Contains(sitename))
                    user.Sites.Add(new Site { Name = sitename });
            }
            ((User)this["user"]).Id = doc.Descendants("user").ElementAt(0).Attribute("id").Value;
        }

    }
}
