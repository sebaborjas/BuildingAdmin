using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDataAccess;
using Services;
using Domain;

namespace TestServices
{
    [TestClass]
    public class TestCategoryService
    {
        private CategoryService _service;
        private Mock<IGenericRepository<Category>> _categoryRepositoryMock;

        [TestInitialize]
        public void SetUp()
        {
            _categoryRepositoryMock = new Mock<IGenericRepository<Category>>(MockBehavior.Strict);
        }

        [TestMethod]
        public void TestCreateCategory()
        {
            _service = new CategoryService(_categoryRepositoryMock.Object);
            _categoryRepositoryMock.Setup(r => r.Insert(It.IsAny<Category>())).Verifiable();

            var category = _service.CreateCategory("Test");
            _categoryRepositoryMock.VerifyAll();

            Assert.AreEqual("Test", category.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateCategoryWithNullName()
        {
            _service = new CategoryService(_categoryRepositoryMock.Object);
            _categoryRepositoryMock.Setup(r => r.Insert(It.IsAny<Category>())).Verifiable();

            var category = _service.CreateCategory(null);
            _categoryRepositoryMock.VerifyAll();

            Assert.IsNull(category.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateCategoryWithEmptyName()
        {
            _service = new CategoryService(_categoryRepositoryMock.Object);
            _categoryRepositoryMock.Setup(r => r.Insert(It.IsAny<Category>())).Verifiable();

            var category = _service.CreateCategory("");
            _categoryRepositoryMock.VerifyAll();

            Assert.AreEqual("", category.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateCategoryWithWhitespaceName()
        {
            _service = new CategoryService(_categoryRepositoryMock.Object);
            _categoryRepositoryMock.Setup(r => r.Insert(It.IsAny<Category>())).Verifiable();

            var category = _service.CreateCategory("     ");
            _categoryRepositoryMock.VerifyAll();

            Assert.AreEqual("     ", category.Name);
        }

        [TestMethod]
        public void TestGetAllCategories()
        {
            var categories = new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Name = "Electricista"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Fontanero"
                },
                new Category()
                {
                    Id = 3,
                    Name = "Plomero"
                }
            };
            _categoryRepositoryMock.Setup(r => r.GetAll<Category>()).Returns(categories);
            _service = new CategoryService(_categoryRepositoryMock.Object);

            var result = _service.GetAll();

            _categoryRepositoryMock.VerifyAll();
            CollectionAssert.AreEqual(categories, result);
        }

        [TestMethod]
        public void TestGetCategory()
        {
            var category = new Category()
            {
                Id = 1,
                Name = "Electricista"
            };
            _categoryRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns(category);
            _service = new CategoryService(_categoryRepositoryMock.Object);

            var result = _service.Get(1);

            _categoryRepositoryMock.VerifyAll();
            Assert.AreEqual(category, result);
        }

        [TestMethod]
        public void TestCreateCategoryWithParent()
        {
            var parentCategory = new Category()
            {
                Id = 1,
                Name = "Jardines",
                RelatedTo = null
            };
            _categoryRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns(parentCategory); 
            _categoryRepositoryMock.Setup(r=>r.Insert(It.IsAny<Category>())).Verifiable();
            _service = new CategoryService(_categoryRepositoryMock.Object);

            var result = _service.CreateCategory("Jardineria", 1);

            _categoryRepositoryMock.VerifyAll();
            Assert.AreEqual(result.RelatedTo, parentCategory);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateCategoryWithInvalidParent()
        {
            _categoryRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns((Category)null);
            _service = new CategoryService(_categoryRepositoryMock.Object);

            var result = _service.CreateCategory("Jardineria", 123);

            _categoryRepositoryMock.VerifyAll();
        }
    }

}

