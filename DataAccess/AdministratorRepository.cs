using DataAccess;

namespace TestDataAccess;

public class AdministratorRepository
{
    private readonly BuildingAdminContext _context;

    public AdministratorRepository(BuildingAdminContext context)
    {
        _context = context;
    }
  
}