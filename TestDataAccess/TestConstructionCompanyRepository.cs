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
    [TestClass]
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
                    Name="Construction Divisions SA"
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

        [TestMethod]
        public void TestInsert()
        {
            ConstructionCompany company = new ConstructionCompany
            {
                Id = 3,
                Name = "Norte Construcciones"
            };

            _repository.Insert(company);

            Assert.AreEqual(company, _context.ConstructionCompanies.Find(3));
        }

        [TestMethod]
        public void TestGet()
        {
            var constructionCompanies = Data();
            LoadConext(constructionCompanies);

            var constructionCompany = _repository.Get(1);

            Assert.AreEqual(constructionCompany, constructionCompanies[0]);

        }

        [TestMethod]
        public void TestGetNotFound()
        {
            var constructionCompanies = Data();
            LoadConext(constructionCompanies);

            var constructionCompany = _repository.Get(5);

            Assert.IsNull(constructionCompany);
        }

        [TestMethod]
        public void TestGetAll()
        {
            var constructionCompanies = Data();
            LoadConext(constructionCompanies);

            var gerConstructionCompanies = _repository.GetAll<ConstructionCompany>().ToList();

            CollectionAssert.AreEqual(constructionCompanies, gerConstructionCompanies);
        }

        [TestMethod]
        public void TestUpdate()
        {
            var constructionCompanies = Data();
            LoadConext(constructionCompanies);

            var constructionCompany = _context.ConstructionCompanies.Find(1);

            string newName = "Building SA";

            constructionCompany.Name = newName;

            _repository.Update(constructionCompany);

            Assert.AreEqual(newName, _context.ConstructionCompanies.Find(1).Name);
        }

        [TestMethod]
        public void TestDelete()
        {
            var constructionCompanies = Data();
            LoadConext(constructionCompanies);

            var constructionCompany = _context.ConstructionCompanies.Find(1);

            _repository.Delete(constructionCompany);

            Assert.IsNull(_context.ConstructionCompanies.Find(1));

        }
    }
}
