using AppPets.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppPets.Viewmodels
{
    public class PetsViewModel : BaseViewModel
    {

        List<PetModel> _PetsList;
        public List<PetModel> PetsList
        {
            get => _PetsList;
            set => SetProperty(ref _PetsList, value);
        }

        public PetsViewModel()
        {
            App.Pets = new List<PetModel>
            {
                new PetModel
                {
                    ID = 1,
                    Name = "Dante",
                    Age = 12,
                    Breed = "Labrador",
                    Picture = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse4.mm.bing.net%2Fth%3Fid%3DOIP.yOsv0W4wnDncLprPl2cRMgHaK6%26pid%3DApi&f=1"
                },
                new PetModel
                {
                    ID = 2,
                    Name = "Kitty",
                    Age = 10,
                    Breed = "Snauzer",
                    Picture = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse1.mm.bing.net%2Fth%3Fid%3DOIP.X85MEmp4hP4_tsy_BmTC_wHaJQ%26pid%3DApi&f=1"
                },
                new PetModel
                {
                    ID = 3,
                    Name = "Simba",
                    Age = 1,
                    Breed = "Pastor Belga",
                    Picture = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse1.mm.bing.net%2Fth%3Fid%3DOIP.FDJ7iq8UgNQzAv6n1ytb9wHaJ4%26pid%3DApi&f=1"
                }
            };

            PetsList = App.Pets;
        }
    }
}
