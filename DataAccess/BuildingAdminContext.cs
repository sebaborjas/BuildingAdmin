using Microsoft.EntityFrameworkCore;
using Domain;

namespace DataAccess
{
    public class BuildingAdminContext : DbContext
    {
        public BuildingAdminContext(DbContextOptions<BuildingAdminContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
