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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

    [TestMethod]
    public void TestModifyConstructionCompany()
    {
        var constructionCompany = new ConstructionCompany
        {
            Id = 1,
            Name = "Test Construction Company"
        };

        _constructionCompanyServiceMock.Setup(x => x.ModifyConstructionCompany("Test Construction Company")).Returns(constructionCompany);

        var constructionCompanyController = new ConstructionCompanyController(_constructionCompanyServiceMock.Object);
        var result = constructionCompanyController.ModifyConstructionCompany(new ModifyConstructionCompanyInput { Name = "Test Construction Company" });

        var okResult = result as OkObjectResult;

        var constructionCompanyModel = okResult.Value as ConstructionCompanyOutput;
        Assert.AreEqual(constructionCompany.Id, constructionCompanyModel.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestModifyConstructionCompanyWithNullInput()
    {
        var constructionCompanyController = new ConstructionCompanyController(_constructionCompanyServiceMock.Object);
        _constructionCompanyServiceMock.Setup(x => x.ModifyConstructionCompany(null)).Throws(new ArgumentNullException("name", "Invalid construction company"));

        var result = constructionCompanyController.ModifyConstructionCompany(new ModifyConstructionCompanyInput());

        _constructionCompanyServiceMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestModifyConstructionCompanyWithEmptyName()
    {
        _constructionCompanyServiceMock
            .Setup(x => x.ModifyConstructionCompany(It.Is<string>(s => string.IsNullOrEmpty(s))))
            .Throws(new ArgumentNullException("name", "Invalid construction company"));

        var constructionCompanyController = new ConstructionCompanyController(_constructionCompanyServiceMock.Object);

        var input = new ModifyConstructionCompanyInput { Name = "" };

        var result = constructionCompanyController.ModifyConstructionCompany(input);

        _constructionCompanyServiceMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestModifyConstructionCompanyWithWhiteSpaceName()
    {
        _constructionCompanyServiceMock
            .Setup(x => x.ModifyConstructionCompany(It.Is<string>(s => string.IsNullOrWhiteSpace(s))))
            .Throws(new ArgumentNullException("name", "Invalid construction company"));

        var constructionCompanyController = new ConstructionCompanyController(_constructionCompanyServiceMock.Object);

        var input = new ModifyConstructionCompanyInput { Name = "   " };

        var result = constructionCompanyController.ModifyConstructionCompany(input);

        _constructionCompanyServiceMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestModifyConstructionCompanyWithExistingName()
    {
        _constructionCompanyServiceMock
            .Setup(x => x.ModifyConstructionCompany("Test Construction Company"))
            .Throws(new ArgumentException("Construction company already exist"));

        var constructionCompanyController = new ConstructionCompanyController(_constructionCompanyServiceMock.Object);

        var input = new ModifyConstructionCompanyInput { Name = "Test Construction Company" };

        var result = constructionCompanyController.ModifyConstructionCompany(input);

        _constructionCompanyServiceMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestModifyConstructionCompanyWithNoCurrentUser()
    {
        _constructionCompanyServiceMock
            .Setup(x => x.ModifyConstructionCompany("Test Construction Company"))
            .Throws(new InvalidOperationException("Current user is not authorized to update a construction company"));

        var constructionCompanyController = new ConstructionCompanyController(_constructionCompanyServiceMock.Object);

        var input = new ModifyConstructionCompanyInput { Name = "Test Construction Company" };

        var result = constructionCompanyController.ModifyConstructionCompany(input);

        _constructionCompanyServiceMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestModifyConstructionCompanyWithNoConstructionCompany()
    {
        _constructionCompanyServiceMock
            .Setup(x => x.ModifyConstructionCompany("Test Construction Company"))
            .Throws(new InvalidOperationException("Current user does not have a construction company"));

        var constructionCompanyController = new ConstructionCompanyController(_constructionCompanyServiceMock.Object);

        var input = new ModifyConstructionCompanyInput
        {
            Name = "Test Construction Company"
        };

        var result = constructionCompanyController.ModifyConstructionCompany(input);

        _constructionCompanyServiceMock.VerifyAll();
    }

    [TestMethod]
    public void TestGetUserConstructionCompany()
    {
        var company = new ConstructionCompany
        {
            Id = 1,
            Name = "Empresa"
        };
        _constructionCompanyServiceMock.Setup(r=>r.GetUserCompany()).Returns(company);
        var constructionCompanyController = new ConstructionCompanyController(_constructionCompanyServiceMock.Object);
        
        var result = constructionCompanyController.GetUserCompany();
        
        var okResult = result as OkObjectResult;
        var companyReturned = okResult.Value as ConstructionCompanyOutput;
        _constructionCompanyServiceMock.VerifyAll();
        Assert.AreEqual(companyReturned.Id, company.Id);
        Assert.AreEqual(companyReturned.Name, company.Name);

    }

    [TestMethod]
    [ExpectedException(typeof(KeyNotFoundException))]
    public void TestNonExistenUserCompany()
    {
        _constructionCompanyServiceMock.Setup(r => r.GetUserCompany()).Throws(new KeyNotFoundException());
        var constructionCompanyController = new ConstructionCompanyController(_constructionCompanyServiceMock.Object);

        var result = constructionCompanyController.GetUserCompany();
    }

}


