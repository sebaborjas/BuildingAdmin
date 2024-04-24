using Domain;
using IServices;
using WebApi;
using Moq;
using DTO.In;
using Microsoft.AspNetCore.Mvc;
using DTO.Out;

namespace TestWebApi;

[TestClass]
public class TestAdministratorController
{
  private Mock<IService<Administrator>> _administratorServiceMock;

  [TestInitialize]
  public void SetUp(){
    _administratorServiceMock = new Mock<IService<Administrator>>(MockBehavior.Strict);
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

    _administratorServiceMock.Setup(r => r.Create(It.IsAny<Administrator>())).Returns(admin);


    var administratorController = new AdministratorController(_administratorServiceMock.Object);

    var result = administratorController.Create(administratorCreateModel);
    var okResult = result as OkObjectResult;
    var administratorModel = okResult.Value as AdministratorModel;

    var expectedContent = new AdministratorModel(admin);

    

    _administratorServiceMock.VerifyAll();
    Assert.AreEqual(administratorModel, expectedContent);
  }
}