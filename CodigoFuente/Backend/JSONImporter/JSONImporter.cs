using Domain;
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

            Dictionary<string, List<CreateBuildingInput>> buildingsInput;
            try
            {
                buildingsInput = JsonSerializer.Deserialize<Dictionary<string, List<CreateBuildingInput>>>(jsonFile, settings);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                return new List<CreateBuildingInput>();
            }

            if (buildingsInput == null || !buildingsInput.ContainsKey("buildings"))
            {
                Console.WriteLine("The key 'edificios' was not found in the JSON.");
                return new List<CreateBuildingInput>();
            }

            return buildingsInput["buildings"];
        }
    }
}
