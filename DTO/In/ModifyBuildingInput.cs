using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.In
{
    public class ModifyBuildingInput
    {
        public string ConstructionCompany {  get; set; }
        public float Expenses { get; set; }
        public List<ModifyApartmentInput> Apartments { get; set; }

        public Building ToEntity()
        {
            var apartments = new List<Apartment>();
            foreach (var apartment in Apartments)
            {
                apartments.Add(apartment.ToEntity());
            }
            return new Building()
            {
                ConstructionCompany = new ConstructionCompany() { Name = ConstructionCompany },
                Expenses = Expenses,
                Apartments = apartments
            };
        }
    }
}
