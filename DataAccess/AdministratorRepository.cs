using DataAccess;
using Domain;

namespace TestDataAccess;

public class AdministratorRepository : GenericRepository<Administrator>
{
    private readonly BuildingAdminContext _context;

    public AdministratorRepository(BuildingAdminContext context)
    {
        _context = context;
    }
  
}