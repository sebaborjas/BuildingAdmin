using DataAccess;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Factory
{
  public class ServicesFactory
  {
    private readonly IServiceCollection _services;

    public ServicesFactory(IServiceCollection services)
    {
      _services = services;
    }

    public void AddCustomServices()
    {
    
    }

    public void AddDbContextService()
    {
      _services.AddDbContext<DbContext, BuildingAdminContext>();
    }
  }
}