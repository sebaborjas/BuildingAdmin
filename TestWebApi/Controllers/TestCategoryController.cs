using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using IServices;
using WebApi;
using Moq;
using DTO.In;
using Microsoft.AspNetCore.Mvc;

namespace TestWebApi;

[TestClass]
public class TestCategoryController
{
    private Mock<ICategoryService> _categoryServiceMock;

    [TestInitialize]
    public void SetUp()
    {
        _categoryServiceMock = new Mock<ICategoryService>(MockBehavior.Strict);
    }

    [TestMethod]
    public void TestCreateCategory()
    {
        var category = new Category() { Name = "Test" };
        var createCategoryModel = new CreateCategoryModel() { Name = "Test" };

        _categoryServiceMock.Setup(x => x.CreateCategory("Test")).Returns(category);

        var categoryController = new CategoryController(_categoryServiceMock.Object);

        var result = categoryController.CreateCategory(createCategoryModel);
        var okResult = result as OkObjectResult;
        var categoryResult = okResult.Value as Category;
        Assert.IsNotNull(categoryResult);
        Assert.AreEqual(categoryResult.Name, "Test");
    }

    [TestMethod]
    public void TestCreateCategoryWithNullInput()
    {
        var categoryController = new CategoryController(_categoryServiceMock.Object);

        var result = categoryController.CreateCategory(null);
        Assert.IsInstanceOfType(result, typeof(BadRequestResult));
    }

    [TestMethod]
    public void TestCreateCategoryWithEmptyName()
    {
        var categoryController = new CategoryController(_categoryServiceMock.Object);

        var result = categoryController.CreateCategory(new CreateCategoryModel());
        Assert.IsInstanceOfType(result, typeof(BadRequestResult));
    }

}
