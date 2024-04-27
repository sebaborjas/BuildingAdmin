using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.In
{
    public class CreateBuildingInput
    {
        public CreateBuildingInput()
        {
            Apartments = new List<NewApartmentInput>();
        }

        public string Name {  get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string ConstructionCompany { get; set; }
        public float Expenses { get; set; }
        public List<NewApartmentInput> Apartments { get; set; }

        public Building ToEntity()
        {
            var apartments = new List<Apartment>();
            foreach (var apartment in Apartments)
            {
                var owner = new Owner()
                {
                    Name = apartment.OwnerName,
                    LastName = apartment.OwnerLastName,
                    Email = apartment.OwnerEmail,
                };
                apartments.Add(new Apartment()
                {
                    Bathrooms = apartment.Bathrooms,
                    DoorNumber = apartment.DoorNumber,
                    Floor = apartment.Floor,
                    HasTerrace = apartment.HasTerrace,
                    Owner = owner,
                    Rooms = apartment.Rooms
                });
            };
            return new Building()
            {
                Address = Address,
                Name = Name,
                Apartments = apartments,
                ConstructionCompany = new ConstructionCompany() { Name = ConstructionCompany },
                Expenses = Expenses,
                Location = Location
            };
        }

    }
}
