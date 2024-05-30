namespace IServices;
using Domain;

public interface IUserServices
{
    public Administrator CreateAdministrator(Administrator administrator);

    public MaintenanceOperator CreateMaintenanceOperator(MaintenanceOperator maintenanceOperator);

    public void DeleteManager(int id);

    public CompanyAdministrator CreateCompanyAdministrator(CompanyAdministrator companyAdministrator);

}