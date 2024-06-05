using System.Reflection;
using IServices;
using IImporter;
using Domain;
using DTO.In;
using DTO.Out;
using IDataAccess;


namespace Services
{
    public class ImportService : IImportService
    {
        private IBuildingService _buildingService;
        private ISessionService _sessionService;
        private IGenericRepository<Owner> _ownerRepository;
        private IGenericRepository<Manager> _managerRepository;

        private string ImportersPath = "./Importers";

        public ImportService(IBuildingService buildingService, ISessionService sessionService, IGenericRepository<Owner> ownerRepository, IGenericRepository<Manager> managerRepository)
        {
            _buildingService = buildingService;
            _sessionService = sessionService;
            _ownerRepository = ownerRepository;
            _managerRepository = managerRepository;
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

        public ImporterOutput ImportBuildings(string importerName, string path)
        {
            if (string.IsNullOrEmpty(importerName) || string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("Importer name and path cannot be null");
            }

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found");
            }

            if (!Directory.GetFiles(ImportersPath).Any())
            {
                throw new InvalidOperationException("No importers found");
            }

            if (!GetAllImporters().Any(i => i.GetName() == importerName))
            {
                throw new FileNotFoundException($"Importer with name '{importerName}' not found");
            }

            ImporterInterface importer = GetAllImporters().FirstOrDefault(i => i.GetName() == importerName);
            if (importer == null)
            {
                throw new InvalidOperationException("Error creating importer");
            }

            try
            {
                var importerInput = new ImporterInput { ImporterName = importerName, Path = path };

                var buildings = importer.ImportFile(importerInput);
                if (buildings == null)
                {
                    throw new InvalidOperationException("Error importing buildings");
                }

                var createdBuildingsIds = new List<CreateBuildingOutput>();
                var errors = new List<string>();

                foreach (CreateBuildingInput building in buildings)
                {
                    var apartments = new List<Apartment>();
                    foreach (NewApartmentInput apartment in building.Apartments)
                    {

                        Apartment apartmentToCreate = new Apartment
                        {
                            DoorNumber = apartment.DoorNumber,
                            Floor = apartment.Floor,
                            HasTerrace = apartment.HasTerrace,
                            Rooms = apartment.Rooms,
                            Bathrooms = apartment.Bathrooms
                        };

                        var owner = _ownerRepository.GetByCondition(o => o.Email == apartment.OwnerEmail);
                        if (owner != null)
                        {
                            apartmentToCreate.Owner = owner;
                        }

                        apartments.Add(apartmentToCreate);
                    }

                    try
                    {
                        var buildingToCreate = new Building
                        {
                            Name = building.Name,
                            Address = building.Address,
                            Location = building.Location,
                            Expenses = building.Expenses,
                            Tickets = new List<Ticket>(),
                            Apartments = apartments
                        };
                        if (building.ManagerEmail != null)
                        {
                            var manager = _managerRepository.GetByCondition(m => m.Email == building.ManagerEmail);
                            if (manager != null)
                            {
                                manager.Buildings.Add(buildingToCreate);
                                _managerRepository.Update(manager);
                            }
                        }

                        var newBuilding = _buildingService.CreateBuilding(buildingToCreate);
                        var response = new CreateBuildingOutput(newBuilding);
                        createdBuildingsIds.Add(response);
                    }
                    catch (Exception e)
                    {
                        errors.Add(building.Name);
                    }

                }
                return new ImporterOutput { CreatedBuildings = createdBuildingsIds, Errors = errors };
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Error creating building", e);
            }
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
