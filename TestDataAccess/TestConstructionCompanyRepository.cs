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
    public class TestConstructionCompanyRepository
    {
        private BuildingAdminContext _context;

        private SqliteConnection _connection;

        private ConstructionCompanyRepository _repository;

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

            _repository = new ConstructionCompanyRepository(_context);
        }

        private List<ConstructionCompany> Data()
        {
            List<ConstructionCompany> list = new List<ConstructionCompany>
            {
                new ConstructionCompany
                {
                    Id = 1,
                    Name = "Construction Group",
                },

                new ConstructionCompany{
                    Id = 2,
                    Name="Construction Divisions S.A."
                }

            };

            return list;
        }

        private void LoadConext(List<ConstructionCompany> list)
        {
            _context.ConstructionCompanies.AddRange(list);
            _context.SaveChanges();
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            _connection.Close();
        }
    }
}
