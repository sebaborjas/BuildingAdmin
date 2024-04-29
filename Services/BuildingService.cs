using IServices;
using Domain;
using IDataAccess;
using IDataAcess;

namespace Services;

public class BuildingService : IBuildingService
{

  private readonly IGenericRepository<Building> _buildingRepository;

  public BuildingService(IGenericRepository<Building> buildingRepository)
  {
    _buildingRepository = buildingRepository;
  }

  public Building CreateBuilding(Building building)
  {
    var buildingAlreadyExist = _buildingRepository.GetByCondition(b => b.Name == building.Name);

    if(buildingAlreadyExist != null)
    {
      throw new ArgumentException("Building already exist");
    }

    _buildingRepository.Insert(building);
    return building;

  }

  public void DeleteBuilding(int id)
  {
    Building buildingToDelete = _buildingRepository.Get(id);

    if(buildingToDelete == null)
    {
      throw new ArgumentNullException("Building not found");
    }

    _buildingRepository.Delete(buildingToDelete);
  }

  public Building ModifyBuilding(int id,  Building modifiedBuilding)
  {
    Building buildingToModify = _buildingRepository.Get(id) ?? throw new ArgumentNullException("Building not found");

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