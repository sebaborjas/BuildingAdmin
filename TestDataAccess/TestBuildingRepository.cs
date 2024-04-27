using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Domain;

namespace TestDataAccess
{
    [TestClass]
    public class TestBuildingRepository
    {
        private BuildingAdminContext _context;
        private SqliteConnection _connection;
        private BuildingRepository _repository;
        private ConstructionCompany _constructionCompany;

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

            _repository = new BuildingRepository(_context);

            _constructionCompany = new ConstructionCompany
            {
                Id = 1,
                Name = "Constructorea SA",
            };
        }

        private List<Building> Data()
        {
            List<Building> list = new List<Building>
            {
                new Building
                {
                    Id = 1,
                    Name = "Building Central",
                    Address = "Calle, 1111, esquina",
                    Location = "1.234, 1.234",
                    Expenses = 1000,
                    ConstructionCompany = _constructionCompany,
                    Apartments = new List<Apartment>(),
                    Tickets = new List<Ticket>()
                },
                new Building
                {
                    Id = 2,
                    Name = "Building Norte",
                    Address = "Calle, 2222, esquina",
                    Location = "2.234, 2.234",
                    Expenses = 2000,
                    ConstructionCompany = _constructionCompany,
                    Apartments = new List<Apartment>(),
                    Tickets = new List<Ticket>()
                }
                
            };

            return list;
        }

        private void LoadContext(List<Building> list)
        {
            _context.Buildings.AddRange(list);
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
