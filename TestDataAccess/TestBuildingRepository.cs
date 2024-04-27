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
        }
    }
}
