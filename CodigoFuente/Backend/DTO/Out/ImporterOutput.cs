using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Out
{
    public class ImporterOutput
    {
        public List<CreateBuildingOutput> CreatedBuildings { get; set; }
        public List<string> Errors { get; set; }

        public ImporterOutput()
        {
            CreatedBuildings = new List<CreateBuildingOutput>();
            Errors = new List<string>();
        }

        public void AddBuilding(Building building)
        {
            CreatedBuildings.Add(new CreateBuildingOutput(building));
        }

        public void AddError(string buildingName)
        {
            Errors.Add(buildingName);
        }
    }
}
