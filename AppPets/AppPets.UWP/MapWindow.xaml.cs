﻿using AppPets.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace AppPets.UWP
{
    public sealed partial class MapWindow : UserControl
    {
        public MapWindow(PetModel pet)
        {
            this.InitializeComponent();

            WindowPicture.Source = new BitmapImage(new Uri(pet.Picture));
            WindowName.Text = pet.Name;
            WindowAge.Text = pet.Age.ToString() + " años";
            WindowBreed.Text = pet.Breed;
        }
    }
}
