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
        public void ListAllAdministrators()
        {
            
        }
    }
}