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
using DTO.Out;
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

        var category = new Category
        {
            Id = 1,
            Name = "Test Category"
        };

        _categoryServiceMock.Setup(x => x.CreateCategory("Test Category")).Returns(category);

        var categoryController = new CategoryController(_categoryServiceMock.Object);
        var result = categoryController.CreateCategory(new CreateCategoryModel { Name = "Test Category" });
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.IsInstanceOfType(okResult.Value, typeof(CategoryModel));
        
        var categoryModel = okResult.Value as CategoryModel;
        Assert.IsNotNull(categoryModel);
        Assert.AreEqual(category.Id, categoryModel.Id);
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
