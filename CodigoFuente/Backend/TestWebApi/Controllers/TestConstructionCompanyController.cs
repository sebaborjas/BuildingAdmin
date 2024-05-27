using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using IServices;
using WebApi.Controllers;
using Moq;
using DTO.In;
using DTO.Out;
using Microsoft.AspNetCore.Mvc;

namespace TestWebApi;

[TestClass]
public class TestConstructionCompanyController
{
    private Mock<IConstructionCompanyService> _constructionCompanyServiceMock;

    [TestInitialize]
    public void SetUp()
    {
        _constructionCompanyServiceMock = new Mock<IConstructionCompanyService>(MockBehavior.Strict);
    }

    [TestMethod]
    public void TestCreateConstructionCompany()
    {

        var constructionCompany = new ConstructionCompany
        {
            Id = 1,
            Name = "Test Construction Company"
        };

        _constructionCompanyServiceMock.Setup(x => x.CreateConstructionCompany("Test Construction Company")).Returns(constructionCompany);

        var constructionCompanyController = new ConstructionCompanyController(_constructionCompanyServiceMock.Object);
        var result = constructionCompanyController.CreateConstructionCompany(new CreateConstructionCompanyInput { Name = "Test Construction Company" });

        var okResult = result as OkObjectResult;

        var constructionCompanyModel = okResult.Value as ConstructionCompanyOutput;
        Assert.AreEqual(constructionCompany.Id, constructionCompanyModel.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestCreateConstructionCompanyWithNullInput()
    {
        var constructionCompanyController = new ConstructionCompanyController(_constructionCompanyServiceMock.Object);
        _constructionCompanyServiceMock.Setup(x => x.CreateConstructionCompany(null)).Throws(new ArgumentNullException("name", "Invalid construction company"));

        var result = constructionCompanyController.CreateConstructionCompany(new CreateConstructionCompanyInput());

        _constructionCompanyServiceMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestCreateConstructionCompanyWithEmptyName()
    {
        _constructionCompanyServiceMock
            .Setup(x => x.CreateConstructionCompany(It.Is<string>(s => string.IsNullOrEmpty(s))))
            .Throws(new ArgumentNullException("name", "Invalid construction company"));

        var constructionCompanyController = new ConstructionCompanyController(_constructionCompanyServiceMock.Object);

        var input = new CreateConstructionCompanyInput { Name = "" };

        var result = constructionCompanyController.CreateConstructionCompany(input);

        _constructionCompanyServiceMock.VerifyAll();
    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestCreateConstructionCompanyWithWhiteSpaceName()
    {
        _constructionCompanyServiceMock
            .Setup(x => x.CreateConstructionCompany(It.Is<string>(s => string.IsNullOrWhiteSpace(s))))
            .Throws(new ArgumentNullException("name", "Invalid construction company"));

        var constructionCompanyController = new ConstructionCompanyController(_constructionCompanyServiceMock.Object);

        var input = new CreateConstructionCompanyInput { Name = "   " };

        var result = constructionCompanyController.CreateConstructionCompany(input);

        _constructionCompanyServiceMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestCreateConstructionCompanyWithExistingName()
    {
        _constructionCompanyServiceMock
            .Setup(x => x.CreateConstructionCompany("Test Construction Company"))
            .Throws(new ArgumentException("Construction company already exist"));

        var constructionCompanyController = new ConstructionCompanyController(_constructionCompanyServiceMock.Object);

        var input = new CreateConstructionCompanyInput { Name = "Test Construction Company" };

        var result = constructionCompanyController.CreateConstructionCompany(input);

        _constructionCompanyServiceMock.VerifyAll();
    }
}
