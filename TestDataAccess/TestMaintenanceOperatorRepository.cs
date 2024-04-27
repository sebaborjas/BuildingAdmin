using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Domain;
using DataAccess;

namespace TestDataAccess
{
    [TestClass]
    public class TestMaintenanceOperatorRepository
    {
        private BuildingAdminContext _context;
        private SqliteConnection _connection;
        private MaintenanceOperatorRepository _repository;
        private ConstructionCompany constructionCompany;
        private Building building;


        [TestInitialize]
        public void SetUp()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<BuildingAdminContext>()
                .UseSqlite(_connection)
                .Options;

            _context = new BuildingAdminContext(options);
            _context.Database.EnsureCreated();

            _repository = new MaintenanceOperatorRepository(_context);

            constructionCompany = new ConstructionCompany
            {
                Id = 1,
                Name = "Constructora 1"
            };

            building = new Building
            {
                Id = 1,
                Name = "Edificio 1",
                Address = "Calle 1",
                Expenses = 1000,
                Location = "1.000, 1.000",
                ConstructionCompany = constructionCompany,
                Tickets = new List<Ticket>(),
                Apartments = new List<Apartment>(),
            };
        }

        private List<MaintenanceOperator> Data()
        {
            List<MaintenanceOperator> list = new List<MaintenanceOperator>
            {
                new MaintenanceOperator
                {
                    Id = 1,
                    Name = "Seba",
                    LastName = "Borjas",
                    Email = "sebaQborjas.com",
                    building = building
                },
                new MaintenanceOperator
                {
                    Id = 2,
                    Name = "Rodri",
                    LastName = "Conze",
                    Email = "rodriQconze.com",
                    building = building
                }

            };
            return list;

        }

        private void LoadConext(List<MaintenanceOperator> list)
        {
            _context.MaintenanceOperators.AddRange(list);
            _context.SaveChanges();
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            _connection.Close();
        }
    }
}

