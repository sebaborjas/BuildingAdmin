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
        public float? Expenses { get; set; }
        public List<ModifyApartmentInput> Apartments { get; set; } = new List<ModifyApartmentInput>();

        public Building ToEntity()
        {
            var newBuilding = new Building();
            
            if(this.Expenses != null)
            {
                newBuilding.Expenses = (float)this.Expenses;
            }
            
            var apartments = new List<Apartment>();
            foreach (var apartment in this.Apartments)
            {
                apartments.Add(apartment.ToEntity());
            }
            newBuilding.Apartments = apartments;

            return newBuilding;
        }
    }
}
