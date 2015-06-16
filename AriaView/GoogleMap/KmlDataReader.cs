using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using AriaView.WebService;
using AriaView.Model;

namespace AriaView.GoogleMap
{
    /// <summary>
    /// Parse le fichier kml et contient les données permettant de construire la carte.
    /// </summary>
    public class KmlDataReader
    {
        private XDocument doc;
        private string webServiceURL;
        private List<Site> sites;
        private List<string> dates;
        //public List<String> ImagesNameList
        //{
        //    get { return imagesNameList; }
        //    set
        //    {
        //        imagesNameList = value;
        //    }
        //}
        //private List<string> imagesNameList;

        public KmlDataReader(XDocument doc,string webServiceURL,List<Site> sites,List<String> dates)
        {
            this.doc = doc;
            this.webServiceURL = webServiceURL;
            this.dates = dates;
            this.sites = sites;
            //imagesNameList = GetImagesName();
        }

        private List<string> GetImagesName()
        {
            XNamespace xmlns = doc.Root.Name.Namespace;
            return doc.Descendants(xmlns + "GroundOverlay")
                .Descendants(xmlns + "Icon")
                .Descendants(xmlns + "href")
                .Select(X => X.Value).ToList();
        }

        public AriaViewDate CreateAriaViewDate()
        {
            try
            {
                XNamespace xmlns = doc.Root.Name.Namespace;
                var latLonBoxElement = doc.Descendants(xmlns + "LatLonBox")
                    .ElementAt(0);
                var north = XmlConvert.ToDouble(latLonBoxElement.Descendants(xmlns + "north")
                    .ElementAt(0)
                    .Value);
                var east = XmlConvert.ToDouble(latLonBoxElement.Descendants(xmlns + "east")
                    .ElementAt(0)
                    .Value);
                var south = XmlConvert.ToDouble(latLonBoxElement.Descendants(xmlns + "south")
                    .ElementAt(0)
                    .Value);
                var west = XmlConvert.ToDouble(latLonBoxElement.Descendants(xmlns + "west")
                    .ElementAt(0)
                    .Value);
                
                return new AriaViewDate(north, east, south, west, CreatePollutantList(),sites,dates);
            }
            catch(Exception e)
            {
                return null;
            }
        }

        //private List<AriaViewDateTerm> CreateDateTermsList()
        //{
        //    XNamespace xmlns = doc.Root.Name.Namespace;
        //    var termsList = new List<AriaViewDateTerm>();
        //    foreach(var groundOverlayElement in doc.Descendants(xmlns + "GroundOverlay"))
        //    {
        //        var rawName = groundOverlayElement.Descendants(xmlns + "name")
        //            .ElementAt(0)
        //            .Value;
        //        var startDate = groundOverlayElement.Descendants(xmlns + "TimeSpan")
        //            .ElementAt(0)
        //            .Descendants(xmlns + "begin")
        //            .ElementAt(0)
        //            .Value;
        //        var endDate = groundOverlayElement.Descendants(xmlns + "TimeSpan")
        //          .ElementAt(0)
        //          .Descendants(xmlns + "end")
        //          .ElementAt(0)
        //          .Value;
        //        var imgName = groundOverlayElement.Descendants(xmlns + "Icon")
        //         .ElementAt(0)
        //         .Descendants(xmlns + "href")
        //         .ElementAt(0)
        //         .Value;
        //        termsList.Add(new AriaViewDateTerm(rawName, startDate, endDate, webServiceURL + "/" + imgName));
        //    }
        //    return termsList;
        //}

        private List<AriaViewDateTerm> CreateDateTermsList(XElement FolderElement)
        {
            XNamespace xmlns = doc.Root.Name.Namespace;
            var termsList = new List<AriaViewDateTerm>();
            foreach (var groundOverlayElement in FolderElement.Descendants(xmlns + "GroundOverlay"))
            {
                var rawName = groundOverlayElement.Descendants(xmlns + "name")
                    .ElementAt(0)
                    .Value;
                var startDate = groundOverlayElement.Descendants(xmlns + "TimeSpan")
                    .ElementAt(0)
                    .Descendants(xmlns + "begin")
                    .ElementAt(0)
                    .Value;
                var endDate = groundOverlayElement.Descendants(xmlns + "TimeSpan")
                  .ElementAt(0)
                  .Descendants(xmlns + "end")
                  .ElementAt(0)
                  .Value;
                var imgName = groundOverlayElement.Descendants(xmlns + "Icon")
                 .ElementAt(0)
                 .Descendants(xmlns + "href")
                 .ElementAt(0)
                 .Value;
                termsList.Add(new AriaViewDateTerm(rawName, startDate, endDate, webServiceURL + "/" + imgName));
            }
            return termsList;
        }

        private List<Pollutant> CreatePollutantList()
        {
            var list = new List<Pollutant>();
            XNamespace xmlns = doc.Root.Name.Namespace;
            var FolderNodes = doc.Descendants(xmlns + "Folder")
                .Where(X => X.Descendants(xmlns + "ScreenOverlay").Count() > 0);
            foreach(var node in FolderNodes)
            {
                var pollutantName = node
                    .Descendants(xmlns + "Folder")
                    .Descendants(xmlns + "name").ElementAt(0).Value;
                var dateTerms = CreateDateTermsList(node);
                list.Add(new Pollutant(pollutantName, dateTerms));
            }
            return list;
        }


        /// <summary>
        /// Renvoie l'URL de l'image en légende
        /// </summary>
        /// <returns></returns>
        public String GetLegendImage()
        {
             XNamespace xmlns = doc.Root.Name.Namespace;
             var imageUrl = doc.Descendants(xmlns + "Folder")
                 .ElementAt(0)
                 .Descendants(xmlns + "ScreenOverlay")
                 .Descendants(xmlns + "href")
                 .ElementAt(0)
                 .Value;
                return webServiceURL + "/" + imageUrl;
        }

    }
}
