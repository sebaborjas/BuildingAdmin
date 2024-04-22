using Domain;
using DataAccess;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace TestDataAccess
{
    [TestClass]
    public class TestAdministratorRepository
    {
        private BuildingAdminContext _context;

        private SqliteConnection _connection;

        private AdministratorRepository _repository;

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

            _repository = new AdministratorRepository(_context);
        }

        private List<Administrator> Data()
        {
            List<Administrator> list = new List<Administrator>
            {
                new Administrator
                {
                    Id = 1,
                    Name = "John",
                    LastName = "Doe",
                    Email = "test@test.com",
                    Password = "Prueba.1234"
                },

                new Administrator
                {
                    Id = 2,
                    Name = "Test",
                    LastName = "Test",
                    Email = "prueba@test.com",
                    Password = "Prueba.1234"
                },

                new Administrator
                {
                    Id = 3,
                    Name = "Admin",
                    LastName = "Building",
                    Email = "prueba@adinet.com",
                    Password = "Prueba.1234"
                },
            };

            return list;
        }

        private void LoadConext( List<Administrator> list)
        {
            _context.Administrators.AddRange(list);
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
            Administrator admin = new Administrator
            {
                Id = 4,
                Name = "Jonny",
                LastName = "Bravo",
                Email = "nuevo@test.com",
                Password = "Prueba.1234"
            };

            _repository.Insert(admin);

            Assert.AreEqual(admin, _context.Administrators.Find(4));
        }

        [TestMethod]
        public void TestGet()
        {
            var adminList = Data();
            LoadConext(adminList);

            var getAdmin = _repository.Get(1);

            Assert.AreEqual(getAdmin, adminList[0]);
            
        }

        [TestMethod]
        public void TestGetAll()
        {
            var adminList = Data();
            LoadConext(adminList);

            var getAdmins = _repository.GetAll<Administrator>().ToList();

            CollectionAssert.AreEqual(adminList, getAdmins);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
            var adminList = Data();
            LoadConext(adminList);

            Administrator admin = new Administrator
            {
                Id = 1,
                Name = "Nuevo Nombre",
                LastName = "Nuevo Apellido",
                Email = "nuevo@test.com",
                Password = "Prueba.1234"
            };

            _repository.Update(admin);

            Assert.AreEqual(admin, _context.Administrators.Find(1));   
        }

        [TestMethod]
        public void TestDelete()
        {
            
        }
    }
}