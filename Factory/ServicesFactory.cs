using DataAccess;
using IDataAcess;
using IServices;
using Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Domain;
using IDataAccess;

namespace Factory
{
  public class ServicesFactory
  {
    private IServiceCollection _services;

    public ServicesFactory(IServiceCollection services)
    {
      _services = services;
    }

    public void AddCustomServices()
    {
      _services.AddScoped<IUserServices, UserService>();
      _services.AddScoped<ISessionService, SessionService>();
    }

    public void AddDbContextService()
    {
      _services.AddDbContext<DbContext, BuildingAdminContext>();
      _services.AddScoped<IGenericRepository<Administrator>, AdministratorRepository>();
      _services.AddScoped<IGenericRepository<MaintenanceOperator>, MaintenanceOperatorRepository>();
      _services.AddScoped<IGenericRepository<Manager>, ManagerRepository>();
      _services.AddScoped<ISessionRepository, SessionRepository>();
    }
  }
}