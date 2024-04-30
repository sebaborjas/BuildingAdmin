using IServices;
using Domain;
using IDataAccess;
using IDataAcess;

namespace Services;

public class BuildingService : IBuildingService
{

    private IGenericRepository<Building> _buildingRepository;

    public BuildingService(IGenericRepository<Building> buildingRepository)
    {
        _buildingRepository = buildingRepository;
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
        Building buildingToDelete = _buildingRepository.Get(buildingId) ?? throw new ArgumentNullException("Building not found");

        _buildingRepository.Delete(buildingToDelete);
    }

    public Building ModifyBuilding(int buildingId, Building modifiedBuilding)
    {
        Building buildingToModify = _buildingRepository.Get(buildingId) ?? throw new ArgumentNullException("Building not found");

        buildingToModify.Name = modifiedBuilding.Name;
        buildingToModify.Address = modifiedBuilding.Address;
        buildingToModify.Location = modifiedBuilding.Location;
        buildingToModify.ConstructionCompany = modifiedBuilding.ConstructionCompany;
        buildingToModify.Expenses = modifiedBuilding.Expenses;
        buildingToModify.Apartments = modifiedBuilding.Apartments;
        buildingToModify.Tickets = modifiedBuilding.Tickets;


        _buildingRepository.Update(buildingToModify);

        return buildingToModify;
    }
}