using AppPets.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppPets.Viewmodels
{
    public class PetsDetailViewModel : BaseViewModel
    {
        PetModel _Pet;
        public PetModel Pet
        {
            get => _Pet;
            set => SetProperty(ref _Pet, value);
        }

        public PetsDetailViewModel()
        {
            
        }

        public PetsDetailViewModel(PetModel pet)
        {
            Pet = pet;
        }

    }
}
