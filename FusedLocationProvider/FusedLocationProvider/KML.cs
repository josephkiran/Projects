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
        [XmlRoot(ElementName = "Icon", Namespace = "http://www.opengis.net/kml/2.2")]
        public class Icon
        {
            [XmlElement(ElementName = "href", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Href { get; set; }
        }

        [XmlRoot(ElementName = "IconStyle", Namespace = "http://www.opengis.net/kml/2.2")]
        public class IconStyle
        {
            [XmlElement(ElementName = "Icon", Namespace = "http://www.opengis.net/kml/2.2")]
            public Icon Icon { get; set; }
        }

        [XmlRoot(ElementName = "Style", Namespace = "http://www.opengis.net/kml/2.2")]
        public class Style
        {
            [XmlElement(ElementName = "IconStyle", Namespace = "http://www.opengis.net/kml/2.2")]
            public IconStyle IconStyle { get; set; }
            [XmlAttribute(AttributeName = "id")]
            public string Id { get; set; }
            [XmlElement(ElementName = "LineStyle", Namespace = "http://www.opengis.net/kml/2.2")]
            public LineStyle LineStyle { get; set; }
            [XmlElement(ElementName = "PolyStyle", Namespace = "http://www.opengis.net/kml/2.2")]
            public PolyStyle PolyStyle { get; set; }
            [XmlElement(ElementName = "BalloonStyle", Namespace = "http://www.opengis.net/kml/2.2")]
            public BalloonStyle BalloonStyle { get; set; }
        }

        [XmlRoot(ElementName = "LineStyle", Namespace = "http://www.opengis.net/kml/2.2")]
        public class LineStyle
        {
            [XmlElement(ElementName = "width", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Width { get; set; }
            [XmlElement(ElementName = "color", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Color { get; set; }
        }

        [XmlRoot(ElementName = "PolyStyle", Namespace = "http://www.opengis.net/kml/2.2")]
        public class PolyStyle
        {
            [XmlElement(ElementName = "color", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Color { get; set; }
        }

        [XmlRoot(ElementName = "BalloonStyle", Namespace = "http://www.opengis.net/kml/2.2")]
        public class BalloonStyle
        {
            [XmlElement(ElementName = "text", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "LookAt", Namespace = "http://www.opengis.net/kml/2.2")]
        public class LookAt
        {
            [XmlElement(ElementName = "longitude", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Longitude { get; set; }
            [XmlElement(ElementName = "latitude", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Latitude { get; set; }
            [XmlElement(ElementName = "altitude", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Altitude { get; set; }
            [XmlElement(ElementName = "heading", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Heading { get; set; }
            [XmlElement(ElementName = "tilt", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Tilt { get; set; }
            [XmlElement(ElementName = "range", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Range { get; set; }
        }

        [XmlRoot(ElementName = "Point", Namespace = "http://www.opengis.net/kml/2.2")]
        public class Point
        {
            [XmlElement(ElementName = "coordinates", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Coordinates { get; set; }
            [XmlElement(ElementName = "altitudeMode", Namespace = "http://www.opengis.net/kml/2.2")]
            public string AltitudeMode { get; set; }
            [XmlElement(ElementName = "extrude", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Extrude { get; set; }
        }

        [XmlRoot(ElementName = "Placemark", Namespace = "http://www.opengis.net/kml/2.2")]
        public class Placemark
        {
            [XmlElement(ElementName = "name", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Name { get; set; }
            [XmlElement(ElementName = "description", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Description { get; set; }
            [XmlElement(ElementName = "Point", Namespace = "http://www.opengis.net/kml/2.2")]
            public Point Point { get; set; }
            [XmlElement(ElementName = "visibility", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Visibility { get; set; }
            [XmlElement(ElementName = "LookAt", Namespace = "http://www.opengis.net/kml/2.2")]
            public LookAt LookAt { get; set; }
            [XmlElement(ElementName = "styleUrl", Namespace = "http://www.opengis.net/kml/2.2")]
            public string StyleUrl { get; set; }
            [XmlElement(ElementName = "LineString", Namespace = "http://www.opengis.net/kml/2.2")]
            public LineString LineString { get; set; }
            [XmlElement(ElementName = "Polygon", Namespace = "http://www.opengis.net/kml/2.2")]
            public Polygon Polygon { get; set; }
        }

        [XmlRoot(ElementName = "Folder", Namespace = "http://www.opengis.net/kml/2.2")]
        public class Folder
        {
            [XmlElement(ElementName = "name", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Name { get; set; }
            [XmlElement(ElementName = "description", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Description { get; set; }
            [XmlElement(ElementName = "LookAt", Namespace = "http://www.opengis.net/kml/2.2")]
            public LookAt LookAt { get; set; }
            [XmlElement(ElementName = "Placemark", Namespace = "http://www.opengis.net/kml/2.2")]
            public List<Placemark> Placemark { get; set; }
            [XmlElement(ElementName = "visibility", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Visibility { get; set; }
            [XmlElement(ElementName = "styleUrl", Namespace = "http://www.opengis.net/kml/2.2")]
            public string StyleUrl { get; set; }
            [XmlElement(ElementName = "Document", Namespace = "http://www.opengis.net/kml/2.2")]
            public Document Document { get; set; }
            [XmlElement(ElementName = "GroundOverlay", Namespace = "http://www.opengis.net/kml/2.2")]
            public GroundOverlay GroundOverlay { get; set; }
            [XmlElement(ElementName = "ScreenOverlay", Namespace = "http://www.opengis.net/kml/2.2")]
            public List<ScreenOverlay> ScreenOverlay { get; set; }
           
        }

        [XmlRoot(ElementName = "Pair", Namespace = "http://www.opengis.net/kml/2.2")]
        public class Pair
        {
            [XmlElement(ElementName = "key", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Key { get; set; }
            [XmlElement(ElementName = "styleUrl", Namespace = "http://www.opengis.net/kml/2.2")]
            public string StyleUrl { get; set; }
        }

        [XmlRoot(ElementName = "StyleMap", Namespace = "http://www.opengis.net/kml/2.2")]
        public class StyleMap
        {
            [XmlElement(ElementName = "Pair", Namespace = "http://www.opengis.net/kml/2.2")]
            public List<Pair> Pair { get; set; }
            [XmlAttribute(AttributeName = "id")]
            public string Id { get; set; }
        }

        [XmlRoot(ElementName = "Document", Namespace = "http://www.opengis.net/kml/2.2")]
        public class Document
        {
            [XmlElement(ElementName = "name", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Name { get; set; }
            [XmlElement(ElementName = "visibility", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Visibility { get; set; }
            [XmlElement(ElementName = "description", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Description { get; set; }
            [XmlElement(ElementName = "LookAt", Namespace = "http://www.opengis.net/kml/2.2")]
            public LookAt LookAt { get; set; }
            [XmlElement(ElementName = "Style", Namespace = "http://www.opengis.net/kml/2.2")]
            public List<Style> Style { get; set; }
            [XmlElement(ElementName = "StyleMap", Namespace = "http://www.opengis.net/kml/2.2")]
            public StyleMap StyleMap { get; set; }
            [XmlElement(ElementName = "Placemark", Namespace = "http://www.opengis.net/kml/2.2")]
            public List<Placemark> Placemarks { get; set; }

            [XmlElement(ElementName = "Folder", Namespace = "http://www.opengis.net/kml/2.2")]
            public List<Folder> FolderChildren { get; set; }
        }

        [XmlRoot(ElementName = "LatLonBox", Namespace = "http://www.opengis.net/kml/2.2")]
        public class LatLonBox
        {
            [XmlElement(ElementName = "north", Namespace = "http://www.opengis.net/kml/2.2")]
            public string North { get; set; }
            [XmlElement(ElementName = "south", Namespace = "http://www.opengis.net/kml/2.2")]
            public string South { get; set; }
            [XmlElement(ElementName = "east", Namespace = "http://www.opengis.net/kml/2.2")]
            public string East { get; set; }
            [XmlElement(ElementName = "west", Namespace = "http://www.opengis.net/kml/2.2")]
            public string West { get; set; }
            [XmlElement(ElementName = "rotation", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Rotation { get; set; }
        }

        [XmlRoot(ElementName = "GroundOverlay", Namespace = "http://www.opengis.net/kml/2.2")]
        public class GroundOverlay
        {
            [XmlElement(ElementName = "name", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Name { get; set; }
            [XmlElement(ElementName = "visibility", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Visibility { get; set; }
            [XmlElement(ElementName = "description", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Description { get; set; }
            [XmlElement(ElementName = "LookAt", Namespace = "http://www.opengis.net/kml/2.2")]
            public LookAt LookAt { get; set; }
            [XmlElement(ElementName = "Icon", Namespace = "http://www.opengis.net/kml/2.2")]
            public Icon Icon { get; set; }
            [XmlElement(ElementName = "LatLonBox", Namespace = "http://www.opengis.net/kml/2.2")]
            public LatLonBox LatLonBox { get; set; }
        }

        [XmlRoot(ElementName = "overlayXY", Namespace = "http://www.opengis.net/kml/2.2")]
        public class OverlayXY
        {
            [XmlAttribute(AttributeName = "x")]
            public string X { get; set; }
            [XmlAttribute(AttributeName = "y")]
            public string Y { get; set; }
            [XmlAttribute(AttributeName = "xunits")]
            public string Xunits { get; set; }
            [XmlAttribute(AttributeName = "yunits")]
            public string Yunits { get; set; }
        }

        [XmlRoot(ElementName = "screenXY", Namespace = "http://www.opengis.net/kml/2.2")]
        public class ScreenXY
        {
            [XmlAttribute(AttributeName = "x")]
            public string X { get; set; }
            [XmlAttribute(AttributeName = "y")]
            public string Y { get; set; }
            [XmlAttribute(AttributeName = "xunits")]
            public string Xunits { get; set; }
            [XmlAttribute(AttributeName = "yunits")]
            public string Yunits { get; set; }
        }

        [XmlRoot(ElementName = "rotationXY", Namespace = "http://www.opengis.net/kml/2.2")]
        public class RotationXY
        {
            [XmlAttribute(AttributeName = "x")]
            public string X { get; set; }
            [XmlAttribute(AttributeName = "y")]
            public string Y { get; set; }
            [XmlAttribute(AttributeName = "xunits")]
            public string Xunits { get; set; }
            [XmlAttribute(AttributeName = "yunits")]
            public string Yunits { get; set; }
        }

        [XmlRoot(ElementName = "size", Namespace = "http://www.opengis.net/kml/2.2")]
        public class Size
        {
            [XmlAttribute(AttributeName = "x")]
            public string X { get; set; }
            [XmlAttribute(AttributeName = "y")]
            public string Y { get; set; }
            [XmlAttribute(AttributeName = "xunits")]
            public string Xunits { get; set; }
            [XmlAttribute(AttributeName = "yunits")]
            public string Yunits { get; set; }
        }

        [XmlRoot(ElementName = "ScreenOverlay", Namespace = "http://www.opengis.net/kml/2.2")]
        public class ScreenOverlay
        {
            [XmlElement(ElementName = "name", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Name { get; set; }
            [XmlElement(ElementName = "visibility", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Visibility { get; set; }
            [XmlElement(ElementName = "description", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Description { get; set; }
            [XmlElement(ElementName = "Icon", Namespace = "http://www.opengis.net/kml/2.2")]
            public Icon Icon { get; set; }
            [XmlElement(ElementName = "overlayXY", Namespace = "http://www.opengis.net/kml/2.2")]
            public OverlayXY OverlayXY { get; set; }
            [XmlElement(ElementName = "screenXY", Namespace = "http://www.opengis.net/kml/2.2")]
            public ScreenXY ScreenXY { get; set; }
            [XmlElement(ElementName = "rotationXY", Namespace = "http://www.opengis.net/kml/2.2")]
            public RotationXY RotationXY { get; set; }
            [XmlElement(ElementName = "size", Namespace = "http://www.opengis.net/kml/2.2")]
            public Size Size { get; set; }
        }

        [XmlRoot(ElementName = "LineString", Namespace = "http://www.opengis.net/kml/2.2")]
        public class LineString
        {
            [XmlElement(ElementName = "tessellate", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Tessellate { get; set; }
            [XmlElement(ElementName = "coordinates", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Coordinates { get; set; }
            [XmlElement(ElementName = "altitudeMode", Namespace = "http://www.opengis.net/kml/2.2")]
            public string AltitudeMode { get; set; }
            [XmlElement(ElementName = "extrude", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Extrude { get; set; }
        }

        [XmlRoot(ElementName = "LinearRing", Namespace = "http://www.opengis.net/kml/2.2")]
        public class LinearRing
        {
            [XmlElement(ElementName = "coordinates", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Coordinates { get; set; }
        }

        [XmlRoot(ElementName = "outerBoundaryIs", Namespace = "http://www.opengis.net/kml/2.2")]
        public class OuterBoundaryIs
        {
            [XmlElement(ElementName = "LinearRing", Namespace = "http://www.opengis.net/kml/2.2")]
            public LinearRing LinearRing { get; set; }
        }

        [XmlRoot(ElementName = "Polygon", Namespace = "http://www.opengis.net/kml/2.2")]
        public class Polygon
        {
            [XmlElement(ElementName = "extrude", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Extrude { get; set; }
            [XmlElement(ElementName = "altitudeMode", Namespace = "http://www.opengis.net/kml/2.2")]
            public string AltitudeMode { get; set; }
            [XmlElement(ElementName = "outerBoundaryIs", Namespace = "http://www.opengis.net/kml/2.2")]
            public OuterBoundaryIs OuterBoundaryIs { get; set; }
            [XmlElement(ElementName = "innerBoundaryIs", Namespace = "http://www.opengis.net/kml/2.2")]
            public InnerBoundaryIs InnerBoundaryIs { get; set; }
            [XmlElement(ElementName = "tessellate", Namespace = "http://www.opengis.net/kml/2.2")]
            public string Tessellate { get; set; }
        }

        [XmlRoot(ElementName = "innerBoundaryIs", Namespace = "http://www.opengis.net/kml/2.2")]
        public class InnerBoundaryIs
        {
            [XmlElement(ElementName = "LinearRing", Namespace = "http://www.opengis.net/kml/2.2")]
            public LinearRing LinearRing { get; set; }
        }

        [XmlRoot(ElementName = "kml", Namespace = "http://www.opengis.net/kml/2.2")]
        public class Kml
        {
            [XmlElement(ElementName = "Document", Namespace = "http://www.opengis.net/kml/2.2")]
            public Document Document { get; set; }
            [XmlAttribute(AttributeName = "xmlns")]
            public string Xmlns { get; set; }
        }

    }


}