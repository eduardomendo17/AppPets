﻿using AppPets.Models;
using AppPets.Services;
using AppPets.Views;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppPets.Viewmodels
{
    public class PetsDetailViewModel : BaseViewModel
    {
        PetsViewModel PetsViewModel;

        Command _SaveCommand;
        public Command SaveCommand => _SaveCommand ?? (_SaveCommand = new Command(SaveAction));
        /*public Command SaveCommand
        {
            get
            {
                if (_SaveCommand == null) _SaveCommand = new Command(SaveAction);
                return _SaveCommand;
            }
        }*/

        Command _DeleteCommand;
        public Command DeleteCommand => _DeleteCommand ?? (_DeleteCommand = new Command(DeleteAction));

        Command _MapCommand;
        public Command MapCommand => _MapCommand ?? (_MapCommand = new Command(MapAction));

        Command _GetLocationCommand;
        public Command GetLocationCommand=> _GetLocationCommand ?? (_GetLocationCommand = new Command(GetLocationAction));

        Command _TakePictureCommand;
        public Command TakePictureCommand => _TakePictureCommand ?? (_TakePictureCommand = new Command(TakePictureAction));

        Command _SelectPictureCommand;
        public Command SelectPictureCommand => _SelectPictureCommand ?? (_SelectPictureCommand = new Command(SelectPictureAction));

        /*PetModel _Pet;
        public PetModel Pet
        {
            get => _Pet;
            set => SetProperty(ref _Pet, value);
        }*/

        int _PetID;

        string _PetName;
        public string PetName
        {
            get => _PetName;
            set => SetProperty(ref _PetName, value);
        }

        string _PetBreed;
        public string PetBreed
        {
            get => _PetBreed;
            set => SetProperty(ref _PetBreed, value);
        }

        int _PetAge;
        public int PetAge
        {
            get => _PetAge;
            set => SetProperty(ref _PetAge, value);
        }

        string _PetPicture;
        public string PetPicture
        {
            get => _PetPicture;
            set => SetProperty(ref _PetPicture, value);
        }

        double _PetLatitude;
        public double PetLatitude
        {
            get => _PetLatitude;
            set => SetProperty(ref _PetLatitude, value);
        }

        double _PetLongitude;
        public double PetLongitude
        {
            get => _PetLongitude;
            set => SetProperty(ref _PetLongitude, value);
        }

        public PetsDetailViewModel(PetsViewModel petsViewModel)
        {
            PetsViewModel = petsViewModel;
        }

        public PetsDetailViewModel(PetsViewModel petsViewModel, PetModel pet)
        {
            PetsViewModel = petsViewModel;

            //Pet = pet;
            _PetID = pet.ID;
            PetName = pet.Name;
            PetBreed = pet.Breed;
            PetAge = pet.Age;
            PetPicture = pet.Picture;
            PetLongitude = pet.Longitude;
            PetLatitude = pet.Latitude;
        }

        private async void SaveAction()
        {
            /*if (_PetID > 0)
            {
                // Actualizar
                foreach (PetModel pet in App.Pets)
                {
                    if (pet.ID == _PetID)
                    {
                        pet.Name = PetName;
                        pet.Breed = PetBreed;
                        pet.Age = PetAge;
                        pet.Picture = PetPicture;
                        break;
                    }
                }
            }
            else
            {
                // Agregar
                int newID = App.Pets.Count + 1;
                App.Pets.Add(new Models.PetModel
                {
                    ID = newID,
                    Name = PetName,
                    Breed = PetBreed,
                    Age = PetAge,
                    Picture = PetPicture
                });
            }*/

            ApiResponse response;
            try
            {
                PetModel pet = new PetModel 
                {
                    ID = _PetID,
                    Name = PetName,
                    Breed = PetBreed,
                    Age = PetAge,
                    Picture = PetPicture,
                    Latitude = PetLatitude,
                    Longitude = PetLongitude
                };
                if (pet.ID > 0)
                {
                    // Actualizar
                    response = await new ApiService().PutDataAsync("Pets", pet);
                }
                else
                {
                    // Insertar
                    response = await new ApiService().PostDataAsync("Pets", pet);
                }
                if (response == null || !response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("AppPets", $"Error al procesar la mascota {response.Message}", "Ok");
                    return;
                }

                PetsViewModel.PetsRefresh();

                Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void MapAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(
                new PetsMapsView(new PetModel {
                    ID = _PetID,
                    Name = PetName,
                    Breed = PetBreed,
                    Age = PetAge,
                    Picture = PetPicture,
                    Latitude = PetLatitude,
                    Longitude = PetLongitude
                })
            );;
        }

        private async void DeleteAction()
        {
            if (_PetID > 0)
            {
                // Eliminar

                // Opción 1
                /*List<PetModel> petsNew = new List<PetModel>();
                 foreach (PetModel pet in App.Pets)
                 {
                     if (pet.ID != _PetID)
                     {
                         petsNew.Add(pet);
                     }
                 }
                 App.Pets = petsNew;*/

                // Opción 2
                /*App.Pets.Remove(PetSelected);*/

                // Api
                ApiResponse response;
                try
                {
                    response = await new ApiService().DeleteDataAsync("Pets", _PetID);
                    if (response == null || !response.IsSuccess)
                    {
                        await Application.Current.MainPage.DisplayAlert("AppPets", $"Error al eliminar la mascota {response.Message}", "Ok");
                        return;
                    }

                    PetsViewModel.PetsRefresh();

                    Application.Current.MainPage.Navigation.PopAsync();
                }
                catch (Exception exc)
                {
                    await Application.Current.MainPage.DisplayAlert("AppPets", $"Error al eliminar la mascota {exc.Message}", "Ok");
                }
            }
        }

        private async void GetLocationAction()
        {
            try
            {
                PetLatitude = PetLongitude = 0;
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location != null)
                {
                    //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    PetLatitude = location.Latitude;
                    PetLongitude = location.Longitude;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                await Application.Current.MainPage.DisplayAlert("AppPets", $"El GPS no está soportado en el dispositivo ({fnsEx.Message})", "Ok");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                await Application.Current.MainPage.DisplayAlert("AppPets", $"El GPS no está activado en el dispositivo ({fneEx.Message})", "Ok");
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                await Application.Current.MainPage.DisplayAlert("AppPets", $"No se pudo obtener el permiso para las coordenadas ({pEx.Message})", "Ok");
            }
            catch (Exception ex)
            {
                // Unable to get location
                await Application.Current.MainPage.DisplayAlert("AppPets", $"Se generó un error al obtener las coordenadas del dispositivo ({ex.Message})", "Ok");
            }
        }

        private async void TakePictureAction()
        {
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await Application.Current.MainPage.DisplayAlert("AppPets", "No existe cámara disponible en el dispositivo", "Ok");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "AppPets",
                    Name = "PetPicture.jpg"
                });

                if (file == null)
                    return;

                PetPicture = await new ImageService().ConvertImageFilePathToBase64(file.Path); //file.Path;

                //await DisplayAlert("File Location", file.Path, "OK");
                /*image.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });*/
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("AppPets", $"Se generó un error al tomar la fotografía ({ex.Message})", "Ok");
            }
        }

        private async void SelectPictureAction()
        {
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await Application.Current.MainPage.DisplayAlert("AppPets", "No es posible seleccionar fotografías en el dispositivo", "Ok");
                    return;
                }

                var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });

                if (file == null)
                    return;

                PetPicture = await new ImageService().ConvertImageFilePathToBase64(file.Path); //file.Path;

                //await DisplayAlert("File Location", file.Path, "OK");
                /*image.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });*/
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("AppPets", $"Se generó un error al tomar la fotografía ({ex.Message})", "Ok");
            }
        }

    }
}
