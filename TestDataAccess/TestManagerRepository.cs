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

        [TestMethod]
        public void TestInsert()
        {
            Manager manager = new Manager
            {
                Id = 1,
                Name = "Luis Perez",
                Email = "luis@test.com",
                Password = "Prueba.1234"
            };

            _repository.Insert(manager);

            Assert.AreEqual(manager, _context.Managers.Find(0));
        }
    }
}
