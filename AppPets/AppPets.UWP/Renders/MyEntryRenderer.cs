﻿using AppPets.UWP.Renders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.UWP;
using Windows.UI;
using AppPets.Renders;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace AppPets.UWP.Renders
{
    public class MyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Background = new Windows.UI.Xaml.Media.SolidColorBrush(Colors.Yellow);
                Control.BackgroundFocusBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Colors.Yellow);
            }
        }
    }
}
