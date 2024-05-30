using Domain;
using IServices;
using WebApi.Controllers;
using Moq;
using DTO.In;
using Microsoft.AspNetCore.Mvc;
using DTO.Out;

namespace TestWebApi;

[TestClass]
public class TestUserController
{
    private Mock<IUserServices> _userServiceMock;
    private Mock<ISessionService> _sessionServiceMock;

    [TestInitialize]
    public void SetUp()
    {
        _userServiceMock = new Mock<IUserServices>(MockBehavior.Strict);
        _sessionServiceMock = new Mock<ISessionService>(MockBehavior.Strict);
    }

    [TestMethod]
    public void TestCreateAdministrator()
    {
        Administrator admin = new Administrator
        {
            Id = 1,
            Name = "John",
            LastName = "Doe",
            Email = "test@test.com",
            Password = "Prueba.1234"
        };

        var administratorCreateModel = new AdministratorCreateModel
        {
            Name = "John",
            LastName = "Doe",
            Email = "test@test.com",
            Password = "Prueba.1234"
        };

        _userServiceMock.Setup(r => r.CreateAdministrator(It.IsAny<Administrator>())).Returns(admin);


        var userController = new UserController(_userServiceMock.Object, _sessionServiceMock.Object);

        var result = userController.CreateAdministrator(administratorCreateModel);
        var okResult = result as OkObjectResult;
        var administratorModel = okResult.Value as AdministratorModel;

        var expectedContent = new AdministratorModel(admin);

        _userServiceMock.VerifyAll();
        Assert.AreEqual(administratorModel, expectedContent);
    }

    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void TestCreateAdministratorWithNoBody()
    {
        AdministratorCreateModel administratorCreateModel = null;
        _userServiceMock.Setup(r => r.CreateAdministrator(It.IsAny<Administrator>())).Throws(new NullReferenceException());

        var userController = new UserController(_userServiceMock.Object, _sessionServiceMock.Object);

        var result = userController.CreateAdministrator(administratorCreateModel);
        var okResult = result as OkObjectResult;
        var administratorModel = okResult.Value as AdministratorModel;

        _userServiceMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestCreateAdministratorWithNoName()
    {
        Administrator admin = new Administrator
        {
            LastName = "Doe",
            Email = "test@tes.com"
        };

        AdministratorCreateModel administratorCreateModel = new AdministratorCreateModel
        {
            LastName = admin.LastName,
            Email = admin.Email,
            Password = "Prueba.1234"
        };

        var userController = new UserController(_userServiceMock.Object, _sessionServiceMock.Object);

        var result = userController.CreateAdministrator(administratorCreateModel);
        var okResult = result as OkObjectResult;
        var administratorModel = okResult.Value as AdministratorModel;
        _userServiceMock.VerifyAll();

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestCreateAdministratorWithNoLastName()
    {
        Administrator admin = new Administrator
        {
            Name = "John",
            Email = "test@test.com"
        };

        AdministratorCreateModel administratorCreateModel = new AdministratorCreateModel
        {
            Name = admin.Name,
            Email = admin.Email
        };

        var userController = new UserController(_userServiceMock.Object, _sessionServiceMock.Object);

        var result = userController.CreateAdministrator(administratorCreateModel);

        _userServiceMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestCreateAdministratorWithNoEmail()
    {
        Administrator admin = new Administrator
        {
            Name = "John",
            LastName = "Doe"
        };

        AdministratorCreateModel administratorCreateModel = new AdministratorCreateModel
        {
            Name = admin.Name,
            LastName = admin.LastName
        };

        var userController = new UserController(_userServiceMock.Object, _sessionServiceMock.Object);

        var result = userController.CreateAdministrator(administratorCreateModel);

        _userServiceMock.VerifyAll();

    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestCreateAdministratorWithNoPassword()
    {
        Administrator admin = new Administrator
        {
            Name = "John",
            LastName = "Doe",
            Email = "test@test.com",
        };

        AdministratorCreateModel administratorCreateModel = new AdministratorCreateModel
        {
            Name = admin.Name,
            LastName = admin.LastName,
            Email = admin.Email
        };

        var userController = new UserController(_userServiceMock.Object, _sessionServiceMock.Object);

        var result = userController.CreateAdministrator(administratorCreateModel);

        _userServiceMock.VerifyAll();
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public void TestCreateMaintenanceOperator()
    {
        MaintenanceOperator maintenanceOperator = new MaintenanceOperator
        {
            Id = 1,
            Name = "John",
            LastName = "Doe",
            Email = "test@test.com",
            Password = "Prueba.1234",
            Building = new Building { Id = 1, Name = "Building" }
        };

        var maintenanceOperatorCreateModel = new MaintenanceOperatorCreateModel
        {
            Name = maintenanceOperator.Name,
            LastName = maintenanceOperator.LastName,
            Email = maintenanceOperator.Email,
            Password = maintenanceOperator.Password,
            BuildingId = maintenanceOperator.Building.Id
        };

        _userServiceMock.Setup(r => r.CreateMaintenanceOperator(It.IsAny<MaintenanceOperator>())).Returns(maintenanceOperator);


        var userController = new UserController(_userServiceMock.Object, _sessionServiceMock.Object);

        var result = userController.CreateMaintenanceOperator(maintenanceOperatorCreateModel);
        var okResult = result as OkObjectResult;
        var maintenanceOperatorModel = okResult.Value as MaintenanceOperatorModel;

        var expectedContent = new MaintenanceOperatorModel(maintenanceOperator);



        _userServiceMock.VerifyAll();
        Assert.AreEqual(maintenanceOperatorModel, expectedContent);
    }

    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void TestCreateMaintenanceOperatorWithNoBody()
    {
        MaintenanceOperatorCreateModel maintenanceOperatorCreateModel = null;

        var userController = new UserController(_userServiceMock.Object, _sessionServiceMock.Object);

        var result = userController.CreateMaintenanceOperator(maintenanceOperatorCreateModel);

        _userServiceMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestCreateMaintenanceOperatorWithNoName()
    {
        var maintenanceOperator = new MaintenanceOperator
        {
            LastName = "Doe",
            Email = "test@test.com"
        };

        MaintenanceOperatorCreateModel maintenanceOperatorCreateModel = new MaintenanceOperatorCreateModel
        {
            LastName = maintenanceOperator.LastName,
            Email = maintenanceOperator.Email,
            BuildingId = 1,
            Password = "Pass123.!"
        };

        var userController = new UserController(_userServiceMock.Object, _sessionServiceMock.Object);

        var result = userController.CreateMaintenanceOperator(maintenanceOperatorCreateModel);

        _userServiceMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestCreateMaintenanceOperatorWithNoLastName()
    {
        var maintenanceOperator = new MaintenanceOperator
        {
            Name = "John",
            Email = "test@test.com"
        };

        MaintenanceOperatorCreateModel maintenanceOperatorCreateModel = new MaintenanceOperatorCreateModel
        {
            Name = maintenanceOperator.Name,
            Email = maintenanceOperator.Email,
            BuildingId = 1,
            Password = "Pass123.!"
        };

        var userController = new UserController(_userServiceMock.Object, _sessionServiceMock.Object);

        var result = userController.CreateMaintenanceOperator(maintenanceOperatorCreateModel);

        _userServiceMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestCreateMaintenanceOperatorWithNoEmail()
    {
        _userServiceMock.Setup(r => r.CreateMaintenanceOperator(It.IsAny<MaintenanceOperator>())).Throws(new ArgumentNullException());
        var maintenanceOperator = new MaintenanceOperator
        {
            Name = "John",
            LastName = "Doe"
        };

        MaintenanceOperatorCreateModel maintenanceOperatorCreateModel = new MaintenanceOperatorCreateModel
        {
            Name = maintenanceOperator.Name,
            LastName = maintenanceOperator.LastName,
            BuildingId = 1,
            Password = "Pass123.!"
        };

        var userController = new UserController(_userServiceMock.Object, _sessionServiceMock.Object);

        var result = userController.CreateMaintenanceOperator(maintenanceOperatorCreateModel);

        _userServiceMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestCreateMaintenanceOperatorWithNoBuilding()
    {
        _userServiceMock.Setup(r => r.CreateMaintenanceOperator(It.IsAny<MaintenanceOperator>())).Throws(new ArgumentNullException());
        var maintenanceOperator = new MaintenanceOperator
        {
            Name = "John",
            LastName = "Doe",
            Email = "test@test.com",
            Password = "Prueba.1234",
        };

        MaintenanceOperatorCreateModel maintenanceOperatorCreateModel = new MaintenanceOperatorCreateModel
        {
            Name = maintenanceOperator.Name,
            LastName = maintenanceOperator.LastName,
            Email = maintenanceOperator.Email,
            Password = maintenanceOperator.Password,
        };

        var userController = new UserController(_userServiceMock.Object, _sessionServiceMock.Object);

        var result = userController.CreateMaintenanceOperator(maintenanceOperatorCreateModel);

        _userServiceMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestCreateMaintenanceOperatorWithNoPassword()
    {
        var maintenanceOperator = new MaintenanceOperator
        {
            Name = "John",
            LastName = "Doe",
            Email = "test@test.com",
            Building = new Building()
        };

        MaintenanceOperatorCreateModel maintenanceOperatorCreateModel = new MaintenanceOperatorCreateModel
        {
            Name = maintenanceOperator.Name,
            LastName = maintenanceOperator.LastName,
            Email = maintenanceOperator.Email,
            BuildingId = maintenanceOperator.Building.Id,
        };

        var userController = new UserController(_userServiceMock.Object, _sessionServiceMock.Object);

        var result = userController.CreateMaintenanceOperator(maintenanceOperatorCreateModel);

        _userServiceMock.VerifyAll();
    }

    [TestMethod]
    public void TestDeleteManager()
    {
        int id = 1;

        _userServiceMock.Setup(r => r.DeleteManager(id));

        var userController = new UserController(_userServiceMock.Object, _sessionServiceMock.Object);

        var result = userController.DeleteManager(id);

        _userServiceMock.VerifyAll();

        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestDeleteManagerNotFound()
    {
        int id = 1;

        _userServiceMock.Setup(r => r.DeleteManager(id)).Throws(new ArgumentOutOfRangeException());

        var userController = new UserController(_userServiceMock.Object, _sessionServiceMock.Object);

        var result = userController.DeleteManager(id);

        _userServiceMock.VerifyAll();
    }

    [TestMethod]
    public void TestGetUserByToken(){
        var user = new Administrator
        {
            Name = "John",
            LastName = "Doe",
            Email = "prueba@test.com",
        };

        var userModel = new UserModel(user);

        _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid>())).Returns(user);

        var userController = new UserController(_userServiceMock.Object, _sessionServiceMock.Object);

        string token = Guid.NewGuid().ToString();

        var result = userController.GetUserSession(token);

        _userServiceMock.VerifyAll();

        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }

    [TestMethod]
    public void TestGetUserByTokenNotFound(){

        _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid>())).Returns(() => null);

        var userController = new UserController(_userServiceMock.Object, _sessionServiceMock.Object);

        string token = Guid.NewGuid().ToString();

        var result = userController.GetUserSession(token);

        _userServiceMock.VerifyAll();
        
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public void TestGetUserEmptyToken(){

        _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid>())).Returns(() => null);

        var userController = new UserController(_userServiceMock.Object, _sessionServiceMock.Object);


        var result = userController.GetUserSession("");

        _userServiceMock.VerifyAll();
        
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }
}