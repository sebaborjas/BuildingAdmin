using DataAccess;
using Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDataAccess
{
    [TestClass]
    public class TestApartmentRepository
    {
        private BuildingAdminContext _context;

        private SqliteConnection _connection;

        private ApartmentRepository _repository;

        private List<Apartment> apartments;

        [TestInitialize]
        public void Setup()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<BuildingAdminContext>()
                .UseSqlite(_connection)
                .Options;

            _context = new BuildingAdminContext(options);
            _context.Database.EnsureCreated();
            _repository = new ApartmentRepository(_context);

        }

        [TestMethod]
        public void TestGetAllApartments()
        {

            apartments = new List<Apartment>
            {
                new Apartment()
                {
                    Id = 1,
                    Bathrooms = 1,
                    DoorNumber = 1,
                    Floor = 1,
                    HasTerrace = false,
                    Owner = new Owner(),
                    Rooms = 2
                },
                new Apartment()
                {
                    Id = 2,
                    Bathrooms = 1,
                    DoorNumber = 2,
                    Floor = 1,
                    HasTerrace = true,
                    Owner = new Owner(),
                    Rooms = 2
                },
            };

            _context.Apartments.AddRange(apartments);
            _context.SaveChanges();

            var retrievedApartments = _repository.GetAll<Apartment>();

            CollectionAssert.AreEqual(apartments, retrievedApartments.ToList());
        }
    }
}
