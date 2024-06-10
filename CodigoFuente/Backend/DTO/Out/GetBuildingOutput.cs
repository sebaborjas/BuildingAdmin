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
        public string Location { get; set; }
        public string Address { get; set; }
        public string? ManagerName { get; set; }
        public float Expenses { get; set; }
        public List<GetApartmentOutput> Apartments { get; set; } = new List<GetApartmentOutput>();

        public GetBuildingOutput(Building building, string? manager)
        {
            Id = building.Id;
            Name = building.Name;
            Location = building.Location;
            Address = building.Address;
            Expenses = building.Expenses;
            if (manager != null)
            {
                ManagerName = manager;
            };
            foreach(var apartment in building.Apartments)
            {
                Apartments.Add(new GetApartmentOutput(apartment));
            };
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
