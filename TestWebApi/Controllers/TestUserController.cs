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
  public void TestCreateAdministratorWirhNoBody()
  {
    Administrator admin = new Administrator
    {
      Id = 1,
      Name = "John",
      LastName = "Doe",
      Email = ""
    };

    AdministratorCreateModel administratorCreateModel = null;

    _userServiceMock.Setup(r => r.CreateAdministrator(It.IsAny<Administrator>())).Returns(admin);

    var userController = new UserController(_userServiceMock.Object);

    var result = userController.CreateAdministrator(administratorCreateModel);

    _userServiceMock.VerifyAll();
    Assert.IsInstanceOfType(result, typeof(BadRequestResult));
  }
}