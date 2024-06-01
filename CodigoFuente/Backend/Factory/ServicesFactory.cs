using DataAccess;
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
            _services.AddScoped<IBuildingService, BuildingService>();
            _services.AddScoped<ICategoryService, CategoryService>();
            _services.AddScoped<IInvitationService, InvitationService>();
            _services.AddScoped<IReportServices, ReportsService>();
            _services.AddScoped<ITicketService, TicketService>();
            _services.AddScoped<IImportService, ImportService>();

        }

        public void AddDbContextService()
        {
            _services.AddDbContext<DbContext, BuildingAdminContext>();

            _services.AddScoped<IGenericRepository<Administrator>, AdministratorRepository>();
            _services.AddScoped<IGenericRepository<MaintenanceOperator>, MaintenanceOperatorRepository>();
            _services.AddScoped<IGenericRepository<Manager>, ManagerRepository>();
            _services.AddScoped<IGenericRepository<CompanyAdministrator>, CompanyAdministratorRepository>();

            _services.AddScoped<ISessionRepository, SessionRepository>();

            _services.AddScoped<IGenericRepository<Apartment>, ApartmentRepository>();
            _services.AddScoped<IGenericRepository<Building>, BuildingRepository>();
            _services.AddScoped<IGenericRepository<Category>, CategoryRepository>();
            _services.AddScoped<IGenericRepository<ConstructionCompany>, ConstructionCompanyRepository>();
            _services.AddScoped<IGenericRepository<Invitation>, InvitationRepository>();
            _services.AddScoped<IGenericRepository<Owner>, OwnerRepository>();
            _services.AddScoped<IGenericRepository<Ticket>, TicketRepository>();
        }
    }
}