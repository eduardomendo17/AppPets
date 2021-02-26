using AppPets.Models;
using System;
using System.Collections.Generic;
using System.Text;
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
        }

        private void SaveAction(object obj)
        {
            if (_PetID > 0)
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
            }

            PetsViewModel.PetsRefresh();

            Application.Current.MainPage.Navigation.PopAsync();
        }

        private void DeleteAction(object obj)
        {
            throw new NotImplementedException();
        }

    }
}
