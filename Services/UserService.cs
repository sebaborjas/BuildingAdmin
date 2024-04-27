using IServices;
using Domain;
using IDataAcess;

namespace Services;

public class UserService : IUserServices
{
  private readonly IGenericRepository<Administrator> _adminRepository;
  private readonly IGenericRepository<MaintenanceOperator> _operatorRepository;
  private readonly IGenericRepository<Manager> _managerRepository;

  public UserService(IGenericRepository<Administrator> adminRepository, IGenericRepository<MaintenanceOperator> operatorRepository, IGenericRepository<Manager> managerRepository)
  {
    _adminRepository = adminRepository;
    _operatorRepository = operatorRepository;
    _managerRepository = managerRepository;
  }

  public Administrator CreateAdministrator(Administrator administrator)
  {
    _adminRepository.Insert(administrator);
    return administrator;
  }

  public MaintenanceOperator CreateMaintenanceOperator(MaintenanceOperator maintenanceOperator)
  {
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
