using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using Domain;
using DataAccess;

namespace TestDataAccess
{
    [TestClass]
    public class TestManagerRepository
    {
        private BuildingAdminContext _context;
        private SqliteConnection _connection;
        private ManagerRepository _repository;

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

            _repository = new ManagerRepository(_context);
        }

        private List<Manager> Data()
        {
            List<Manager> list = new List<Manager>
            {
                new Manager
                {
                    Id = 1,
                    Name = "Seba Borjas",
                    Email = "seba@test.com",
                    Password = "SebaB.1234"
                },

                new Manager
                {
                    Id = 2,
                    Name = "Rodri Conze",
                    Email = "rodri@test.com",
                    Password = "Conze.1234"
                },

                new Manager
                {
                    Id = 3,
                    Name = "Agus Martinez",
                    Email = "agus@test.com",
                    Password = "AgusM.1234"
                }
            };

            return list;
        }

        public void LoadConext(List<Manager> list)
        {
            _context.Managers.AddRange(list);
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
        public void TestInsert()
        {
            Manager manager = new Manager
            {
                Id = 4,
                Name = "Luis Perez",
                Email = "luis@test.com",
                Password = "Prueba.1234"
            };

            _repository.Insert(manager);

            Assert.AreEqual(manager, _context.Managers.Find(4));
        }

        [TestMethod]
        public void TestGet()
        {
            var managerList = Data();
            LoadConext(managerList);

            var getManager = _repository.Get(1);

            Assert.AreEqual(getManager, managerList[0]);
        }

        [TestMethod]
        public void TestGetManagerNotFound()
        {
            var managerList = Data();
            LoadConext(managerList);

            var getManager = _repository.Get(4);

            Assert.IsNull(getManager);
        }

        [TestMethod]
        public void TestGetAll()
        {
            var managerList = Data();
            LoadConext(managerList);

            var getAllManagers = _repository.GetAll<Manager>().ToList();

            CollectionAssert.AreEqual(managerList, getAllManagers);
        }

        [TestMethod]
        public void TestGetAllNoList()
        {
            var getManagers = _repository.GetAll<Manager>().ToList();

            int count = getManagers.Count;

            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void TestUpdate()
        {
            var managerList = Data();
            LoadConext(managerList);

            var manager = _context.Managers.Find(1);

            string nuevoEmail = "juanperez@test.com";

            manager.Email = nuevoEmail;

            _repository.Update(manager);

            Assert.AreEqual(nuevoEmail, _context.Managers.Find(1).Email);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateNotFound()
        {
            var managerList = Data();
            LoadConext(managerList);

            var manager = _context.Managers.Find(4);

            _repository.Update(manager);
        }

        [TestMethod]
        public void TestDelete()
        {
            var managerList = Data();
            LoadConext(managerList);

            var manager = _context.Managers.Find(1);

            _repository.Delete(manager);

            Assert.IsNull(_context.Managers.Find(1));

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDeleteNotFound()
        {
            var managerList = Data();
            LoadConext(managerList);

            var manager = _context.Managers.Find(5);

            _repository.Delete(manager);
        }



    }
}
