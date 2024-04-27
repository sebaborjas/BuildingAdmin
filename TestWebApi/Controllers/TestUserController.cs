using Domain;
using IServices;
using WebApi;
using Moq;
using DTO.In;
using Microsoft.AspNetCore.Mvc;
using DTO.Out;

namespace TestWebApi;

[TestClass]
public class TestUserController
{
  private Mock<IUserServices> _userServiceMock;

  [TestInitialize]
  public void SetUp(){
    _userServiceMock = new Mock<IUserServices>(MockBehavior.Strict);
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


    var userController = new UserController(_userServiceMock.Object);

    var result = userController.CreateAdministrator(administratorCreateModel);
    var okResult = result as OkObjectResult;
    var administratorModel = okResult.Value as AdministratorModel;

    var expectedContent = new AdministratorModel(admin);

    

    _userServiceMock.VerifyAll();
    Assert.AreEqual(administratorModel, expectedContent);
  }

  [TestMethod]
  public void TestCreateAdministratorWithNoBody()
  {
    Administrator admin = new Administrator
    {
      Name = "John",
      LastName = "Doe",
      Email = "test@test.com"
    };

    AdministratorCreateModel administratorCreateModel = null;

    var userController = new UserController(_userServiceMock.Object);

    var result = userController.CreateAdministrator(administratorCreateModel);

    _userServiceMock.VerifyAll();
    Assert.IsInstanceOfType(result, typeof(BadRequestResult));
  }

  [TestMethod]
  public void TestCreateAdministratorWithNoName()
  {
    Administrator admin = new Administrator
    {
      LastName = "Doe",
      Email = "test@test.com"
    };

    AdministratorCreateModel administratorCreateModel = new AdministratorCreateModel
    {
      LastName = admin.LastName,
      Email = admin.Email
    };

    var userController = new UserController(_userServiceMock.Object);

    var result = userController.CreateAdministrator(administratorCreateModel);

    _userServiceMock.VerifyAll();
    Assert.IsInstanceOfType(result, typeof(BadRequestResult));
  }

  [TestMethod]
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

    var userController = new UserController(_userServiceMock.Object);

    var result = userController.CreateAdministrator(administratorCreateModel);

    _userServiceMock.VerifyAll();
    Assert.IsInstanceOfType(result, typeof(BadRequestResult));
  }
}