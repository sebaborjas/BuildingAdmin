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

        public GetBuildingOutput(Building building, string? manager)
        {
            this.Id = building.Id;
            this.Name = building.Name;
            this.Location = building.Location;
            this.Address = building.Address;
            if(manager != null)
            {
                this.ManagerName = manager;
            }
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
