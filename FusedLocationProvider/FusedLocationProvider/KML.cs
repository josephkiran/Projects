using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FusedLocationProvider
{
    using System;
    using System.Xml.Serialization;
    using System.Collections.Generic;
    namespace Xml2CSharp
    {
        [XmlRoot(ElementName = "Document")]
        public class Document
        {
            [XmlElement(ElementName = "description")]
            public string Description { get; set; }
            [XmlElement(ElementName = "name")]
            public string Name { get; set; }
            [XmlElement(ElementName = "Placemark")]
            public Placemark Placemark { get; set; }
            [XmlElement(ElementName = "Style")]
            public Style Style { get; set; }
        }

        [XmlRoot(ElementName = "kml")]
        public class Kml
        {
            [XmlElement(ElementName = "Document")]
            public Document Document { get; set; }
        }

        [XmlRoot(ElementName = "LineString")]
        public class LineString
        {
            [XmlElement(ElementName = "altitudeMode")]
            public string AltitudeMode { get; set; }
            [XmlElement(ElementName = "coordinates")]
            public string Coordinates { get; set; }
            [XmlElement(ElementName = "extrude")]
            public string Extrude { get; set; }
            [XmlElement(ElementName = "tessellate")]
            public string Tessellate { get; set; }
        }

        [XmlRoot(ElementName = "LineStyle")]
        public class LineStyle
        {
            [XmlElement(ElementName = "color")]
            public string Color { get; set; }
            [XmlElement(ElementName = "width")]
            public string Width { get; set; }
        }

        [XmlRoot(ElementName = "Placemark")]
        public class Placemark
        {
            [XmlElement(ElementName = "description")]
            public string Description { get; set; }
            [XmlElement(ElementName = "LineString")]
            public LineString LineString { get; set; }
            [XmlElement(ElementName = "name")]
            public string Name { get; set; }
            [XmlElement(ElementName = "styleUrl")]
            public string StyleUrl { get; set; }
        }

        [XmlRoot(ElementName = "PolyStyle")]
        public class PolyStyle
        {
            [XmlElement(ElementName = "color")]
            public string Color { get; set; }
        }

        [XmlRoot(ElementName = "Style")]
        public class Style
        {
            [XmlAttribute(AttributeName = "id")]
            public string Id { get; set; }
            [XmlElement(ElementName = "LineStyle")]
            public LineStyle LineStyle { get; set; }
            [XmlElement(ElementName = "PolyStyle")]
            public PolyStyle PolyStyle { get; set; }
        }

    }

}