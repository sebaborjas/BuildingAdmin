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
    return new NotImplementedException();
  }

  public MaintenanceOperator CreateMaintenanceOperator(MaintenanceOperator maintenanceOperator)
  {
    return new NotImplementedException();
  }

  public void DeleteManager(int id)
  {
    return new NotImplementedException();
  }
}
