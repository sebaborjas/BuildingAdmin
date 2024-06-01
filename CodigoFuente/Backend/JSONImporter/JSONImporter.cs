using System.Collections.Generic;
using System.IO;
using DTO.In;
using IImporter;
using System.Text.Json;

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

            JsonSerializerOptions settings = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            var buildings = JsonSerializer.Deserialize<List<CreateBuildingInput>>(jsonFile, settings);

            return buildings;
        }
    }
}
