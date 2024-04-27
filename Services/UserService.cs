namespace Services;
using IServices;
using Domain;

public class UserService : IUserServices
{
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
