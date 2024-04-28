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
using DTO.Out;

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

        Category category = new Category()
        {
            Name = "Test"
        };

        var createCategoryModel = new CreateCategoryModel()
        {
            Name = "Test"
        };

        _categoryServiceMock.Setup(x => x.CreateCategory(It.IsAny<string>())).Returns(category);
        var result = _categoryServiceMock.Object;
        Assert.IsNotNull(result);

        var controller = new CategoryController(_categoryServiceMock.Object);
        var response = controller.CreateCategory(createCategoryModel) as OkResult;
        Assert.IsNotNull(response);

        _categoryServiceMock.Verify(x => x.CreateCategory(It.IsAny<string>()), Times.Once);
    }
    
}
