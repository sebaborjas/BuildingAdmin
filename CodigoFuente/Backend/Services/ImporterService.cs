using System.Reflection;
using IServices;
using IImporter;
using System.Collections.Generic;
using System.IO;
using Domain;
using System.Security.Cryptography;
using DTO.In;

namespace Services
{
    public class ImportService : IImportService
    {
        private readonly IBuildingService _buildingService;
        private readonly ISessionService _sessionService;

        private string ImportersPath = "./Importers";

        public ImportService(IBuildingService buildingService, ISessionService sessionService)
        {
            _buildingService = buildingService;
            _sessionService = sessionService;
        }

        public List<ImporterInterface> GetAllImporters()
        {
            string[] filePaths = Directory.GetFiles(ImportersPath, "*.dll");

            List<ImporterInterface> availableImporters = new List<ImporterInterface>();

            foreach (string file in filePaths)
            {
                if (FileIsDll(file))
                {
                    FileInfo dllFile = new FileInfo(file);
                    Assembly myAssembly = Assembly.LoadFile(dllFile.FullName);

                    foreach (Type type in myAssembly.GetTypes())
                    {
                        if (ImplementsRequiredInterface(type))
                        {
                            ImporterInterface instance = (ImporterInterface)Activator.CreateInstance(type);
                            availableImporters.Add(instance);
                        }

                    }
                }
            }
            return availableImporters;
        }

        public List<Building> ImportBuildings(string importerName, string path)
        {
            if (string.IsNullOrEmpty(importerName) || string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("Importer name and path cannot be null");
            }

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found");
            }

            if (!Directory.Exists(ImportersPath))
            {
                throw new DirectoryNotFoundException("Importers directory not found");
            }

            if (!Directory.GetFiles(ImportersPath).Any())
            {
                throw new InvalidOperationException("No importers found");
            }

            if (!GetAllImporters().Any())
            {
                throw new InvalidOperationException("No importers found");
            }

            if (!GetAllImporters().Any(i => i.GetName() == importerName))
            {
                throw new FileNotFoundException($"Importer with name '{importerName}' not found");
            }

            if (GetAllImporters().Any(i => i.GetName() == importerName))
            {
                ImporterInterface importer = GetAllImporters().FirstOrDefault(i => i.GetName() == importerName);
                if (importer == null)
                {
                    throw new InvalidOperationException("Error creating importer");
                }
                var currentUser = _sessionService.GetCurrentUser();
                /* CompanyAdministrator companyAdministrator = (CompanyAdministrator)currentUser;

                 if (companyAdministrator.ConstructionCompany == null)
                 {
                     throw new InvalidOperationException("Company administrator does not belong to a construction company");
                 }*/

                try
                {
                    var importerInput = new ImporterInput { ImporterName = importerName, Path = path };

                    List<CreateBuildingInput> buildings = importer.ImportFile(importerInput);
                    Console.WriteLine("Buildings: " + buildings);
                    if(buildings == null)
                    {
                       Console.WriteLine("Buildings not identify");
                    }
                    foreach (CreateBuildingInput building in buildings)
                    {

                        /*if (building == null)
                        {
                            throw new InvalidOperationException("Buildings not identify");
                        }*/
                        List<Apartment> apartments = new List<Apartment>();
                        foreach (NewApartmentInput apartment in building.Apartments)
                        {
                            Apartment apartmentToCreate = new Apartment
                            {
                                DoorNumber = apartment.DoorNumber,
                                Floor = apartment.Floor,
                                HasTerrace = apartment.HasTerrace,
                                Owner = new Owner { Email = apartment.OwnerEmail },
                                Rooms = apartment.Rooms,
                                Bathrooms = apartment.Bathrooms
                            };
                            apartments.Add(apartmentToCreate);

                        }

                        Building buildingToCreate = new Building
                        {
                            Name = building.Name,
                            Address = building.Address,
                            Location = building.Location,
                            Expenses = building.Expenses,
                            Tickets = new List<Ticket>(),
                            Apartments = apartments
                        };

                        _buildingService.CreateBuilding(buildingToCreate);
                        return new List<Building> { buildingToCreate };
                    }
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException("Error creating building", e);
                }
            }
            return new List<Building>();
        }

        private bool FileIsDll(string file)
        {
            return file.EndsWith("dll");
        }

        private bool ImplementsRequiredInterface(Type type)
        {
            return typeof(ImporterInterface).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract;
        }
    }
}
