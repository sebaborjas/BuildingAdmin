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
    throw new NotImplementedException();
  }

  public Building ModifyBuilding(int id,  Building building)
  {
    throw new NotImplementedException();
  }
}