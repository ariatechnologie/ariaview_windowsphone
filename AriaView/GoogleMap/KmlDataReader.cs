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

namespace AriaView.GoogleMap
{
    /// <summary>
    /// Parse le fichier kml et contient les données permettant de construire la carte.
    /// </summary>
    public class KmlDataReader
    {
        private XDocument doc;
        public List<String> ImagesNameList
        {
            get { return imagesNameList; }
            set
            {
                imagesNameList = value;
            }
        }
        private List<string> imagesNameList;

        public KmlDataReader(XDocument doc)
        {
            this.doc = doc;
            imagesNameList = GetImagesName();
        }

        private List<string> GetImagesName()
        {
            XNamespace xmlns = doc.Root.Name.Namespace;
            var t = doc.Descendants(xmlns + "name").ElementAt(0).Value;
            return doc.Descendants(xmlns + "GroundOverlay")
                .Descendants(xmlns + "Icon")
                .Descendants(xmlns + "href")
                .Select(X => X.Value).ToList();
        }

    }
}
