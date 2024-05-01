using IServices;
using Domain;
using IDataAccess;
using IDataAcess;

namespace Services;

public class BuildingService : IBuildingService
{

    private IGenericRepository<Building> _buildingRepository;
    private ISessionService _sessionService;

    public BuildingService(IGenericRepository<Building> buildingRepository, ISessionService sessionService)
    {
        _buildingRepository = buildingRepository;
        _sessionService = sessionService;
    }

    public Building CreateBuilding(Building building)
    {
        var buildingAlreadyExist = _buildingRepository.GetByCondition(b => b.Name == building.Name);

        if (buildingAlreadyExist != null)
        {
            throw new ArgumentException("Building already exist");
        }

        _buildingRepository.Insert(building);
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

        building.Name = modifiedBuilding.Name;
        building.Address = modifiedBuilding.Address;
        building.Location = modifiedBuilding.Location;
        building.ConstructionCompany = modifiedBuilding.ConstructionCompany;
        building.Expenses = modifiedBuilding.Expenses;
        building.Apartments = modifiedBuilding.Apartments;
        building.Tickets = modifiedBuilding.Tickets;

        _buildingRepository.Update(building);

        return building;
    }
}