using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Domain;

namespace DataAccess
{
    public class BuildingAdminContext : DbContext
    {
        public BuildingAdminContext() { }
        
        public BuildingAdminContext(DbContextOptions options) : base(options){ }

        public virtual DbSet<Administrator> Administrators { get; set; }
        public virtual DbSet<Invitation> Invitations { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ConstructionCompany> ConstructionCompanies { get; set; }
        public virtual DbSet<Owner> Owners { get; set; }
        public virtual DbSet<Apartment> Apartments {  get; set; }
        public virtual DbSet<Building> Buildings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string directory = Directory.GetCurrentDirectory();

                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(directory)
                .AddJsonFile("appsettings.json")
                .Build();

                var connectionString = configuration.GetConnectionString(@"BuildingAdmin");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
