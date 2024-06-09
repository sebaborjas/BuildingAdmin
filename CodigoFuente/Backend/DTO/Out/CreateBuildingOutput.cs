using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DTO.Out
{
    public class CreateBuildingOutput
    {
        public CreateBuildingOutput(Building building)
        {
            BuildingId = building.Id;
            Name = building.Name;
        }

        public int BuildingId {  get; set; }
        public string Name { get; set; }
    }
}
