using AppPets.iOS.Renders;
using AppPets.Models;
using AppPets.Renders;
using CoreGraphics;
using Foundation;
using MapKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MyMap), typeof(MyMapRenderer))]
namespace AppPets.iOS.Renders
{
    public class MyMapRenderer : MapRenderer
    {
        UIView customPinView;
        PetModel Pet;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                var nativeMap = Control as MKMapView;
                nativeMap.GetViewForAnnotation = null;
                nativeMap.DidSelectAnnotationView -= OnDidSelectAnnotationView;
                nativeMap.DidDeselectAnnotationView -= OnDidDeselectAnnotationView;
            }

            if (e.NewElement != null)
            {
                Pet = (e.NewElement as MyMap).Pet;

                var nativeMap = Control as MKMapView; //(MKMapView)Control;
                var formsMap = e.NewElement as MyMap; //(MyMap)e.NewElement;
                nativeMap.GetViewForAnnotation = GetViewForAnnotation;
                nativeMap.DidSelectAnnotationView += OnDidSelectAnnotationView;
                nativeMap.DidDeselectAnnotationView += OnDidDeselectAnnotationView;
            }
        }

        protected override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            //return base.GetViewForAnnotation(mapView, annotation);

            MKAnnotationView annotationView = null;
            if (annotation is MKUserLocation) return null;

            annotationView = mapView.DequeueReusableAnnotation(Pet.Name);
            if (annotationView == null)
            {
                annotationView = new MapWindow(annotation, Pet.Name);
                annotationView.Image = UIImage.FromFile("pin2.png");
                annotationView.CalloutOffset = new CGPoint(0, 0);
                ((MapWindow)annotationView).Name = "pet";
            }
            annotationView.CanShowCallout = true;
            return annotationView;
        }

        private void OnDidSelectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            MapWindow mapWindow = e.View as MapWindow;
            customPinView = new UIView();

            if (mapWindow.Name.Equals("pet"))
            {
                customPinView.Frame = new CGRect(0, 0, 150, 84);
                var image = new UIImageView(new CGRect(0, 0, 200, 84));
                image.Image = UIImage.FromFile(Pet.Picture);
                customPinView.AddSubview(image);
                customPinView.Center = new CGPoint(0, -(e.View.Frame.Height + 45));
                e.View.AddSubview(customPinView);
            }
        }

        private void OnDidDeselectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            if (!e.View.Selected)
            {
                customPinView.RemoveFromSuperview();
                customPinView.Dispose();
                customPinView = null;
            }
        }
    }
}