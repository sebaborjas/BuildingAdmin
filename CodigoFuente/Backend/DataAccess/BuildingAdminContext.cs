using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Domain;
using System.Diagnostics.CodeAnalysis;

namespace DataAccess
{
    [ExcludeFromCodeCoverage]
    public class BuildingAdminContext : DbContext
    {
        public BuildingAdminContext() { }

        public BuildingAdminContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Administrator> Administrators { get; set; }
        public virtual DbSet<Invitation> Invitations { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ConstructionCompany> ConstructionCompanies { get; set; }
        public virtual DbSet<Owner> Owners { get; set; }
        public virtual DbSet<MaintenanceOperator> MaintenanceOperators { get; set; }
        public virtual DbSet<Apartment> Apartments { get; set; }
        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<CompanyAdministrator> CompanyAdministrators { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Session>().Navigation(e => e.User).AutoInclude();
            modelBuilder.Entity<Manager>().Navigation(e => e.Buildings).AutoInclude();
            modelBuilder.Entity<CompanyAdministrator>().Navigation(e => e.ConstructionCompany).AutoInclude();
            modelBuilder.Entity<Building>().Navigation(e => e.ConstructionCompany).AutoInclude();
            modelBuilder.Entity<Building>().Navigation(e => e.Apartments).AutoInclude();
            modelBuilder.Entity<Ticket>().Navigation(e => e.Category).AutoInclude();
            modelBuilder.Entity<Building>().Navigation(e => e.Tickets).AutoInclude();
            modelBuilder.Entity<Apartment>().Navigation(e => e.Owner).AutoInclude();
            modelBuilder.Entity<Ticket>().Navigation(e => e.AssignedTo).AutoInclude();
            modelBuilder.Entity<CompanyAdministrator>().Navigation(e => e.ConstructionCompany).AutoInclude();
            modelBuilder.Entity<MaintenanceOperator>().HasMany(e => e.Buildings).WithMany();
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

                optionsBuilder.UseSqlServer(connectionString, o=> o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
            }
        }
    }
}
