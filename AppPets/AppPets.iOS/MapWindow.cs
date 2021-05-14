using Foundation;
using MapKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace AppPets.iOS
{
    public class MapWindow : MKAnnotationView
    {
        public string Name { get; set; }

        public MapWindow(IMKAnnotation annotation, string id) : base(annotation, id)
        {
        }
    }
}