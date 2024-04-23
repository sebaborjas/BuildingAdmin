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


        private List<Category> Data()
        {
            List<Category> listData = new List<Category>
            {
                new Category { Id = 1, Name = "Electricista" },
                new Category { Id = 2, Name = "Plomero" },
                new Category { Id = 3, Name = "Vecino Molesto" }

            };
            return listData;
        }

        private void LoadContext(List<Category> data) {
        
            _context.Categories.AddRange(data);
            _context.SaveChanges();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            _connection.Close();
        }

        [TestMethod]
        public void TestInsert()
        {
            Category category = new Category
            {
                Id = 4,
                Name = "Pintor"
            };

            _categoryRepository.Insert(category);

            Assert.AreEqual(category, _context.Categories.Find(4));
        }

        [TestMethod]
        public void TestGet()
        {
            var categoryList = Data();
            LoadContext(categoryList);

            var getCategory = _categoryRepository.Get(1);

            Assert.AreEqual(getCategory, categoryList[0]);
          
        }

        [TestMethod]
        public void TestGetNotFound()
        {
            var categoryList = Data();
            LoadContext(categoryList);

            var getCategory = _categoryRepository.Get(5);

            Assert.IsNull(getCategory);
        }

        [TestMethod]
        public void TestGetAll()
        {
            var categoryList = Data();
            LoadContext(categoryList);

            var getCategories = _categoryRepository.GetAll<Category>().ToList();

            CollectionAssert.AreEqual(categoryList, getCategories);
        }

        [TestMethod]
        public void TestGetAllNoList()
        {
            var getCategories = _categoryRepository.GetAll<Category>().ToList();

            int count = getCategories.Count;

            Assert.AreEqual(0, count);
        }


        [TestMethod]
        public void TestUpdate()
        {
            var categoryList = Data();
            LoadContext(categoryList);

            var category = _context.Categories.Find(1);

            string newName = "Fontantero";

            category.Name = newName;

            _categoryRepository.Update(category);

            Assert.AreEqual(newName, _context.Categories.Find(1).Name);
        }

        [TestMethod]
        public void TestDelete()
        {
            var categoryList = Data();
            LoadContext(categoryList);

            var category = _context.Categories.Find(2);

            _categoryRepository.Delete(category);

            Assert.IsNull(_context.Categories.Find(2)); 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDeleteNotFound()
        {
            var categories = Data();
            LoadContext(categories);

            var category = _context.Categories.Find(6);

            _categoryRepository.Delete(category);
        }

    }
}
