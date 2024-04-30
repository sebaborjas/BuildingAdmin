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
            _categoryRepositoryMock = new Mock<IGenericRepository<Category>>();
        }

        [TestMethod]
        public void TestCreateCategory()
        {
            _service = new CategoryService(_categoryRepositoryMock.Object);
            _categoryRepositoryMock.Setup(r => r.Insert(It.IsAny<Category>())).Verifiable();

            var category = _service.CreateCategory("Test");
            Assert.IsNotNull(category);
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
            Assert.IsNotNull(category);
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
            Assert.IsNotNull(category);
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
            Assert.IsNotNull(category);
            _categoryRepositoryMock.VerifyAll();

            Assert.AreEqual("     ", category.Name);
        }
    }

}

