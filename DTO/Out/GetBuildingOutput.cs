using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DTO.Out
{
    public class GetBuildingOutput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Expenses { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string ConstructionCompany { get; set; }
        public List<GetApartmentOutput> Apartments { get; set; } = new List<GetApartmentOutput>();

        public GetBuildingOutput(Building building)
        {
            this.Id = building.Id;
            this.Name = building.Name;
            this.Expenses = building.Expenses;
            this.Location = building.Location;
            this.Address = building.Address;
            this.ConstructionCompany = building.ConstructionCompany.Name;
            building.Apartments.ForEach(apartment =>
            {
                this.Apartments.Add(new GetApartmentOutput(apartment));
            });
        }

        public override bool Equals(object obj)
        {
            if (obj is GetBuildingOutput)
            {
                var building = obj as GetBuildingOutput;
                if (building.Id == this.Id)
                {
                    return true;
                };
            }
            return false;
        }
    }
}
