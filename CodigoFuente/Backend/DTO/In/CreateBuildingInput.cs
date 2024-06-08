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
        public float Expenses { get; set; }
        public List<NewApartmentInput> Apartments { get; set; }
        public string ManagerEmail { get; set; } = null;

        public Building ToEntity()
        {
            var apartments = new List<Apartment>();
            foreach (var apartment in Apartments)
            {
                apartments.Add(apartment.ToEntity());
            };
            return new Building()
            {
                Address = Address,
                Name = Name,
                Apartments = apartments,
                Expenses = Expenses,
                Location = Location
            };
        }

    }
}
