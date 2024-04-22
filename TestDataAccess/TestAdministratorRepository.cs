using Domain;
using DataAccess;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

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

            _repository = new AdministratorRepository(_context);
        }

        [TestMethod]
        public void TestInsert()
        {
            Administrator admin = new Administrator
            {
                Id = 1,
                Name = "John",
                LastName = "Doe",
                Email = "test@test.com",
                Password = "Prueba.1234"
            };

            _repository.Insert(admin);

            Assert.AreEqual(admin, _context.Administrators.Find(1));
        }

        [TestMethod]
        public void TestGet()
        {
            
        }

        [TestMethod]
        public void TestGetAll()
        {
            
        }
        
        [TestMethod]
        public void TestUpdate()
        {
            
        }

        [TestMethod]
        public void TestDelete()
        {
            
        }
    }
}