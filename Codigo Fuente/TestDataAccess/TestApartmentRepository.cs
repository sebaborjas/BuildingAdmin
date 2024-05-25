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

            LoadData();
        }

        [TestMethod]
        public void TestGetAllApartments()
        {
            var retrievedApartments = _repository.GetAll<Apartment>();

            CollectionAssert.AreEqual(apartments, retrievedApartments.ToList());
        }

        [TestMethod]
        public void TestGetApartment()
        {
            var firstApartment = apartments.Find(apartment => apartment.Id == 1);

            var retrievedApartment = _repository.Get(1);

            Assert.AreEqual(retrievedApartment, firstApartment);
        }

        [TestMethod]
        public void TestAddApartment()
        {
            var newApartment = new Apartment
            {
                Id = 5,
                Bathrooms = 2,
                DoorNumber = 5,
                Floor = 2,
                HasTerrace = true,
                Owner = new Owner(),
                Rooms = 2
            };

            _repository.Insert(newApartment);
            _repository.Save();
            var retrievedApartment = _context.Apartments.Find(5);

            Assert.AreEqual(retrievedApartment, newApartment);
        }

        [TestMethod]
        public void TestDeleteApartment()
        {
            var apartmentToDelete = _context.Apartments.Find(1);

            _repository.Delete(apartmentToDelete);
            _repository.Save();

            bool exists = _context.Apartments.Find(1) != null;
            Assert.IsFalse(exists);
        }

        [TestMethod]
        public void TestUpdateApartment()
        {
            var apartmentToModify = _context.Apartments.Find(1);

            var newOwner = new Owner();
            apartmentToModify.Rooms = 5;
            apartmentToModify.Bathrooms = 3;
            apartmentToModify.DoorNumber = 5;
            apartmentToModify.HasTerrace = true;
            apartmentToModify.Floor = 3;
            apartmentToModify.Owner = newOwner;
            _repository.Update(apartmentToModify);
            _repository.Save();

            var retrievedApartment = _context.Apartments.Find(1);
            Assert.AreEqual(retrievedApartment, apartmentToModify);
        }

        [TestMethod]
        public void TestCheckConnection()
        {
            bool connected = _repository.CheckConnection();

            Assert.IsTrue(connected);
        }

        private void LoadData()
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
        }
    }
}
