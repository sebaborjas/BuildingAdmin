namespace IServices;
using Domain;

public interface IUserServices
{
  public Administrator CreateAdministrator(Administrator administrator);

  public MaintenanceOperator CreateMaintenanceOperator(MaintenanceOperator maintenanceOperator);
  
}