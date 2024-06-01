using System.Collections.Generic;
using System.IO;
using DTO.In;
using IImporter;
using Newtonsoft.Json;

namespace JSONImporter
{
    public class JSONFileImporter : ImporterInterface
    {
        public string GetName()
        {
            return "JSON";
        }

        public List<CreateBuildingInput> ImportFile(ImporterInput input)
        {
            var jsonFile = File.ReadAllText(input.Path);
            var buildings = JsonConvert.DeserializeObject<List<CreateBuildingInput>>(jsonFile);

            return buildings;
        }
    }
}
