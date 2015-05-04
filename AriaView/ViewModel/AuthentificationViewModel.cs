using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Security.Credentials;
using AriaView.Common;

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
                return;
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

    }
}
