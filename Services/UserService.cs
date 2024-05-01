using IServices;
using Domain;
using IDataAccess;

namespace Services;

public class UserService : IUserServices
{
  private IGenericRepository<Administrator> _adminRepository;
  private IGenericRepository<MaintenanceOperator> _operatorRepository;
  private IGenericRepository<Manager> _managerRepository;
    private ISessionService _sessionService;

  public UserService(IGenericRepository<Administrator> adminRepository, IGenericRepository<MaintenanceOperator> operatorRepository, IGenericRepository<Manager> managerRepository, ISessionService sessionService)
  {
    _adminRepository = adminRepository;
    _operatorRepository = operatorRepository;
    _managerRepository = managerRepository;
        _sessionService = sessionService;
  }

  public Administrator CreateAdministrator(Administrator administrator)
  {
    Administrator administratorAlreadyExist = _adminRepository.GetByCondition(a => a.Email == administrator.Email);

    if(administratorAlreadyExist != null)
    {
      throw new ArgumentException("Administrator already exist");
    }
    _adminRepository.Insert(administrator);
    return administrator;
  }

  public MaintenanceOperator CreateMaintenanceOperator(MaintenanceOperator maintenanceOperator)
  {
    MaintenanceOperator maintenanceOperatorAlreadyExist = _operatorRepository.GetByCondition(a => a.Email == maintenanceOperator.Email);
    if (maintenanceOperatorAlreadyExist != null)
    {
        throw new ArgumentException("Maintenance Operator already exist");
    }
    var operatorBuilding = ((Manager)_sessionService.GetCurrentUser()).Buildings.Find(building => building.Id == maintenanceOperator.Building.Id);
    if(operatorBuilding == null)
    {
      throw new ArgumentException("Invalid building");
    };

    maintenanceOperator.Building = operatorBuilding;
    _operatorRepository.Insert(maintenanceOperator);
    return maintenanceOperator;
    
  }

  public void DeleteManager(int id)
  {
    Manager managerToDelete = _managerRepository.Get(id);

    if(managerToDelete == null)
    {
      throw new ArgumentNullException("Manager not found");
    }
    _managerRepository.Delete(managerToDelete);
  }
}
