using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using DataAccess;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace TestDataAccess
{
    [TestClass]
    public class TestCategoryRepository
    {
        private BuildingAdminContext _context;

        private SqliteConnection _connection;

        private CategoryRepository _categoryRepository;

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

            _categoryRepository = new CategoryRepository(_context);
        }

    }
}
