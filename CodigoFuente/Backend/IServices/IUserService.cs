namespace IServices;
using Domain;
using System.Net;

public interface IUserServices
{
    public Administrator CreateAdministrator(Administrator administrator);

    public MaintenanceOperator CreateMaintenanceOperator(MaintenanceOperator maintenanceOperator);

    public void DeleteManager(int id);

    public CompanyAdministrator CreateCompanyAdministrator(CompanyAdministrator companyAdministrator);

    public List<Manager> GetManagers();

    public List<CompanyAdministrator> GetCompanyAdministrators();

}