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
    public class TestOwnerRepository
    {
        private BuildingAdminContext _context;
        private SqliteConnection _connection;
        private OwnerRepository _repository;

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

            _repository = new OwnerRepository(_context);
        }

        private List<Owner> Data()
        {
            List<Owner> list = new List<Owner>
            {
                new Owner
                {
                    Id = 1,
                    Name = "Seba",
                    LastName = "Borjas",
                    Email = "seba@test.com"
                },
                new Owner
                {
                    Id = 2,
                    Name = "Rodri",
                    LastName = "Conze",
                    Email = "rodri@test.com"
                },
                new Owner
                {
                    Id = 3,
                    Name = "Agus",
                    LastName = "Martinez",
                    Email = "agus@test.com"
                }
            };

            return list;
        }

        public void LoadConext(List<Owner> list)
        {
            _context.Owners.AddRange(list);
            _context.SaveChanges();
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            _connection.Close();
        }

        [TestMethod]
        public void TestInsertOwner()
        {
            Owner owner = new Owner
            {
                Id = 4,
                Name = "Juan",
                LastName = "Perez",
                Email = "juan@test.com"
            };

            _repository.Insert(owner);

            Assert.AreEqual(owner, _context.Owners.Find(4));
        }

        [TestMethod]
        public void TestGet()
        {
            var owners = Data();
            LoadConext(owners);

            var owner = _repository.Get(1);

            Assert.AreEqual(owners[0], owner);
        }

        [TestMethod]
        public void TestGetAll()
        {
            var ownersData = Data();
            LoadConext(ownersData);

            var allOwners = _repository.GetAll<Owner>().ToList();

            CollectionAssert.AreEqual(ownersData, allOwners);
        }


    }
}
