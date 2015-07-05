using AriaView.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Foundation.Collections;

namespace AriaView.Common
{
    /// <summary>
    /// Implémentation de IObservableMap qui prend en charge la réentrance pour une utilisation en tant que modèle d'affichage
    /// par défaut.
    /// </summary>
    public class ObservableDictionary : IObservableMap<string, object>
    {
        private class ObservableDictionaryChangedEventArgs : IMapChangedEventArgs<string>
        {
            public ObservableDictionaryChangedEventArgs(CollectionChange change, string key)
            {
                this.CollectionChange = change;
                this.Key = key;
            }

            public CollectionChange CollectionChange { get; private set; }
            public string Key { get; private set; }
        }

        private Dictionary<string, object> _dictionary = new Dictionary<string, object>();
        public event MapChangedEventHandler<string, object> MapChanged;

        private void InvokeMapChanged(CollectionChange change, string key)
        {
            var eventHandler = MapChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new ObservableDictionaryChangedEventArgs(change, key));
            }
        }

        public void Add(string key, object value)
        {
            this._dictionary.Add(key, value);
            this.InvokeMapChanged(CollectionChange.ItemInserted, key);
        }

        public void Add(KeyValuePair<string, object> item)
        {
            this.Add(item.Key, item.Value);
        }

        public bool Remove(string key)
        {
            if (this._dictionary.Remove(key))
            {
                this.InvokeMapChanged(CollectionChange.ItemRemoved, key);
                return true;
            }
            return false;
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            object currentValue;
            if (this._dictionary.TryGetValue(item.Key, out currentValue) &&
                Object.Equals(item.Value, currentValue) && this._dictionary.Remove(item.Key))
            {
                this.InvokeMapChanged(CollectionChange.ItemRemoved, item.Key);
                return true;
            }
            return false;
        }

        public object this[string key]
        {
            get
            {
                return this._dictionary[key];
            }
            set
            {
                this._dictionary[key] = value;
                this.InvokeMapChanged(CollectionChange.ItemChanged, key);
            }
        }

        public void Clear()
        {
            var priorKeys = this._dictionary.Keys.ToArray();
            this._dictionary.Clear();
            foreach (var key in priorKeys)
            {
                this.InvokeMapChanged(CollectionChange.ItemRemoved, key);
            }
        }

        public ICollection<string> Keys
        {
            get { return this._dictionary.Keys; }
        }

        public bool ContainsKey(string key)
        {
            return this._dictionary.ContainsKey(key);
        }

        public bool TryGetValue(string key, out object value)
        {
            return this._dictionary.TryGetValue(key, out value);
        }

        public ICollection<object> Values
        {
            get { return this._dictionary.Values; }
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return this._dictionary.Contains(item);
        }

        public int Count
        {
            get { return this._dictionary.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return this._dictionary.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this._dictionary.GetEnumerator();
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            int arraySize = array.Length;
            foreach (var pair in this._dictionary)
            {
                if (arrayIndex >= arraySize) break;
                array[arrayIndex++] = pair;
            }
        }

        public void SetDictionary(ObservableDictionary o)
        {
            _dictionary = o._dictionary;
            foreach(var k in Keys)
            {
                this.InvokeMapChanged(CollectionChange.ItemInserted, k);
            }
        }


        /// <summary>
        /// Get the datas to build the map and stores them in the temporary folder
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
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
            var type = doc.Descendants("type").ElementAt(0).Value;
            var site = doc.Descendants("site").ElementAt(0).Value;
            var scale = doc.Descendants("scale").ElementAt(0).Value;
            var model = doc.Descendants("model").ElementAt(0).Value;
            var nest = doc.Descendants("nest").ElementAt(0).Value;
            var strBuilder = new StringBuilder(host);
            var urlParts = new Dictionary<string, string>();
            urlParts.Add("host", host);
            urlParts.Add("url", url);
            urlParts.Add("site", site);
            urlParts.Add("type", type);
            urlParts.Add("scale", scale);
            urlParts.Add("model",model);
            urlParts.Add("nest",nest);
            this["urlParts"] = urlParts;
            

            return String.Format("{0}/{1}/{2}/GEARTH/{3}_{4}/", host, url, site, type, scale);
        }

    }
}
