using Microsoft.EntityFrameworkCore;
using Domain;

namespace DataAccess;

public class AdministratorRepository : GenericRepository<Administrator>
{
    public AdministratorRepository(DbContext context)
    {
        Context = context;
    }
  
}