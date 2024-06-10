using Domain;
using DataAccess;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace TestDataAccess
{
    [TestClass]
    public class TestCompanyAdministratorRepository
    {
        private BuildingAdminContext _context;

        private SqliteConnection _connection;

        private CompanyAdministratorRepository _repository;

        [TestMethod]
        public void TestCompanyAdminRepository()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<BuildingAdminContext>()
                .UseSqlite(_connection)
                .Options;

            _context = new BuildingAdminContext(options);
            _context.Database.EnsureCreated();

            _repository = new CompanyAdministratorRepository(_context);
        }

    }
}
