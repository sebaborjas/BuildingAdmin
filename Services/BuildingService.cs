using IServices;
using Domain;
using IDataAccess;

namespace Services;

public class BuildingService : IBuildingService
{

    private IGenericRepository<Building> _buildingRepository;
    private ISessionService _sessionService;
    private IGenericRepository<Owner> _ownerRepository;
    private IGenericRepository<Manager> _managerRepository;
    private IGenericRepository<ConstructionCompany> _constructionCompany;

    public BuildingService(IGenericRepository<Building> buildingRepository, ISessionService sessionService, IGenericRepository<Owner> ownerRepository, IGenericRepository<Manager> managerRepository, IGenericRepository<ConstructionCompany> constructionCompany)
    {
        _buildingRepository = buildingRepository;
        _sessionService = sessionService;
        _ownerRepository = ownerRepository;
        _managerRepository = managerRepository;
        _constructionCompany = constructionCompany;

    }

    public Building CreateBuilding(Building building)
    {
        var buildingAlreadyExist = _buildingRepository.GetByCondition(b => b.Name == building.Name || b.Address == building.Address || b.Location == building.Location);

        if (buildingAlreadyExist != null)
        {
            throw new ArgumentException("Building already exist");
        }
        var currentUser = _sessionService.GetCurrentUser() as Manager;

        building.ConstructionCompany = getConstructionCompany(building);
        _buildingRepository.Insert(building);
        assignBuildingToManager(building, currentUser);

        return building;

    }

    public void DeleteBuilding(int buildingId)
    {
        var currentUser = _sessionService.GetCurrentUser() as Manager;
        if (currentUser == null)
        {
            throw new InvalidOperationException("Current user is not a manager");
        }

        var building = currentUser.Buildings.FirstOrDefault(b => b.Id == buildingId);
        if (building == null)
        {
            throw new ArgumentNullException("Building not found");
        }

        _buildingRepository.Delete(building);
    }

    public Building ModifyBuilding(int buildingId, Building modifiedBuilding)
    {
        var currentUser = _sessionService.GetCurrentUser() as Manager;
        if (currentUser == null)
        {
            throw new InvalidOperationException("Current user is not a manager");
        }

        var building = currentUser.Buildings.FirstOrDefault(b => b.Id == buildingId);

        if (building == null)
        {
            throw new ArgumentNullException("Building not found");
        }


        if (modifiedBuilding.ConstructionCompany != null)
        {
            building.ConstructionCompany = getConstructionCompany(modifiedBuilding);
        }

        if (modifiedBuilding.Expenses > 0)
        {
            building.Expenses = modifiedBuilding.Expenses;
        }

        ModifyApartments(building.Apartments, modifiedBuilding.Apartments);
        _buildingRepository.Update(building);

        return building;
    }

    public List<Building> GetAllBuildingsForUser()
    {
        return null;
    }

    public Building Get(int id)
    {
        return null;
    }

    private void ModifyApartments(List<Apartment> originalApartments, List<Apartment> modifiedApartments)
    {
        modifiedApartments.ForEach(modifiedApartment =>
        {
            var originalApartment = originalApartments.Find(apartment => apartment.Id == modifiedApartment.Id);
            if (originalApartment != null)
            {
                var ownerToModify = _ownerRepository.GetByCondition(owner => owner.Email == modifiedApartment.Owner.Email);
                if (ownerToModify == null)
                {
                    ownerToModify = modifiedApartment.Owner;
                    _ownerRepository.Insert(ownerToModify);
                }
                originalApartment.Owner = ownerToModify;
            }
        });
    }

    private void assignBuildingToManager(Building building, Manager manager)
    {
        manager.Buildings.Add(building);
        _managerRepository.Update(manager);
    }

    private ConstructionCompany getConstructionCompany(Building building)
    {
        ConstructionCompany constructionCompanyModify = building.ConstructionCompany;
        var constructionCompany = _constructionCompany.GetByCondition(c => c.Name == building.ConstructionCompany.Name);
        if (constructionCompany == null)
        {
            _constructionCompany.Insert(constructionCompanyModify);
            constructionCompany = constructionCompanyModify;
        }
        return constructionCompany;
    }
}