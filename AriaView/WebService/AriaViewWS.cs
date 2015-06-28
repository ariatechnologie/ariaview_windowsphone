using System;
using System.Net.Http;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using AriaView.Model;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace AriaView.WebService
{
    public class AriaViewWS
    {
        public string Url = ApplicationData.Current.LocalSettings.Values["wsurl"] as string;
        
        public  async Task<String> Authentificate(string login,string pwd)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.ExpectContinue = false;
                var content = new FormUrlEncodedContent(new[] 
            {
                new KeyValuePair<string, string>("login", login),
                new KeyValuePair<string,string>("password",pwd)
            });
                var result = await client.PostAsync("/webservices/ARIAVIEW/login.php", content);
                if (result.IsSuccessStatusCode)
                    return await result.Content.ReadAsStringAsync();
                else
                    return null;
            }
            catch (Exception)
            { return null; }
            
        }
        
        public async Task<String> GetSitesInfosAsync(Site site,User user)
        {
            try
            {
                
                var client = new HttpClient();
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.ExpectContinue = false;

                var content = new FormUrlEncodedContent(new[] 
            {
                new KeyValuePair<string, string>("site", site.Name),
                new KeyValuePair<string,string>("login",user.Login),
                new KeyValuePair<string,string>("password",user.Password)
            });
                var result = await client.PostAsync("/webservices/ARIAVIEW/infosite.php", content);
                return await result.Content.ReadAsStringAsync();
            }
            catch (Exception)
            { return null; }
        }

        public async Task<string> GetDatesAsync(string url)
        {
            return await new HttpClient().PostAsync(url,null).Result.Content.ReadAsStringAsync();
        }

        public async Task<string> GetKmlAsync(string url)
        {
            return await new HttpClient().PostAsync(url, null).Result.Content.ReadAsStringAsync();
        }
       
    }
}
