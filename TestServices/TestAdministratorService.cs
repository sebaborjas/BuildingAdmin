using Domain;
using Services;
using Moq;
using IDataAcess;

namespace TestServices;

[TestClass]
public class TestAdministratorService
{
  [TestInitialize]
  public void SetUp()
  {
    // Initialize the test
  }

  [TestMethod]
  public void CreateCorrectAdministrator()
  {
    var mock = new Mock<IGenericRepository<Administrator>>(MockBehavior.Strict);

    mock.Setup(r => r.Save()).Verifiable();

    var service = new AdministratorService(mock.Object);

    var administrator = new Administrator
    {
      Name = "Fernando",
      LastName = "Alonso",
      Email = "elnano@padre.com",
      Password = "Nano.1234"
    };

    mock!.Setup(r => r.Insert(administrator));
    service!.Insert(administrator);

    // Assert
    mock.VerifyAll();
  }

}