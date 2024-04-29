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
    throw new NotImplementedException();
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