using Domain;
using IServices;
using WebApi;
using Moq;
using Microsoft.AspNetCore.Mvc;

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

    _administratorServiceMock.Setup(r => r.Create(admin)).Returns(admin.Id);
     var administratorController = new AdministratorController(_administratorServiceMock.Object);

     var result = administratorController.Create(admin);
     var okResult = result as OkObjectResult;
     var administratorModel = okResult.Value as int?;

     _administratorServiceMock.VerifyAll();
     Assert.AreEqual(administratorModel, admin.Id);


  }
}