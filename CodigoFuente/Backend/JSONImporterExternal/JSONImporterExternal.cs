using DTO.In;
using IImporter;
using Domain;
using System.Text.Json;
using IServices;

namespace JSONImporterExternal
{
    public class JSONFileExternalImporter : ImporterInterface
    {
        private readonly IUserServices _userService;

        public string GetName()
        {
            return "JSON External";
        }

        public List<CreateBuildingInput> ImportFile(ImporterInput input)
        {
            var jsonFile = File.ReadAllText(input.Path);

            JsonSerializerOptions settings = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            Dictionary<string, List<CreateBuildingInputFromExternalJSON>> buildingsInput;
            try
            {
                buildingsInput = JsonSerializer.Deserialize<Dictionary<string, List<CreateBuildingInputFromExternalJSON>>>(jsonFile, settings);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                return new List<CreateBuildingInput>();
            }

            if (buildingsInput == null || !buildingsInput.ContainsKey("edificios"))
            {
                Console.WriteLine("The key 'edificios' was not found in the JSON.");
                return new List<CreateBuildingInput>();
            }

            var buildings = new List<CreateBuildingInput>();
            foreach (var buildingInput in buildingsInput["edificios"])
            {
                if (buildingInput == null || buildingInput.Address == null || buildingInput.Gps == null)
                {
                    Console.WriteLine("One of the building inputs or its required properties is null.");
                    continue;
                }

                var apartments = new List<NewApartmentInput>();
                foreach (var apartment in buildingInput.Apartments)
                {
                    apartments.Add(new NewApartmentInput()
                    {
                        Bathrooms = (short)apartment.Bathrooms,
                        DoorNumber = (short)apartment.DoorNumber,
                        Floor = (short)apartment.Floor,
                        HasTerrace = apartment.HasTerrace,
                        Rooms = (short)apartment.Rooms,
                        OwnerEmail = apartment.OwnerEmail,
                    });
                }

                var building = new CreateBuildingInput()
                {
                    Address = $"{buildingInput.Address.Street}, {buildingInput.Address.Number}, {buildingInput.Address.SecondaryStreet}",
                    Name = buildingInput.Name,
                    Apartments = apartments,
                    Expenses = buildingInput.Expenses,
                    Location = $"{buildingInput.Gps.Latitude},{buildingInput.Gps.Longitude}",
                    ManagerEmail = buildingInput.ManagerEmail
                };

                buildings.Add(building);
            }

            return buildings;
        }
    }
}
