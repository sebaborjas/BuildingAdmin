using Domain;
using Services;
using Moq;
using IDataAcess;

namespace TestServices;

[TestClass]
public class TestUserService
{
  private UserService _service;
  private Mock<IGenericRepository<Administrator>> _adminRepositoryMock;
  private Mock<IGenericRepository<MaintenanceOperator>> _operatorRepositoryMock;
  private Mock<IGenericRepository<Manager>> _managerRepositoryMock;

  [TestInitialize]
  public void SetUp()
  {
    _adminRepositoryMock = new Mock<IGenericRepository<Administrator>>(MockBehavior.Strict);
    _operatorRepositoryMock = new Mock<IGenericRepository<MaintenanceOperator>>();
    _managerRepositoryMock = new Mock<IGenericRepository<Manager>>();
  }

  [TestMethod]
  public void CreateCorrectAdministrator()
  {
    _adminRepositoryMock.Setup(r => r.Insert(It.IsAny<Administrator>())).Verifiable();

    _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object);

    var administrator = new Administrator
    {
      Name = "Fernando",
      LastName = "Alonso",
      Email = "elnano@padre.com",
      Password = "Nano.1234"
    };

    var createdAdmin = _service.CreateAdministrator(administrator);

    _adminRepositoryMock.VerifyAll();
    Assert.AreEqual(administrator, createdAdmin);
  }

  [TestMethod]
  public void CreateCorrectMaintenanceOperator()
  {
    _operatorRepositoryMock.Setup(r => r.Insert(It.IsAny<MaintenanceOperator>())).Verifiable();

    _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object);

    var maintenanceOperator = new MaintenanceOperator
    {
      Name = "Marc",
      LastName = "Marquez",
      Email = "papa@devalentino.es",
      Password = "Honda.1234"
    };

    var createdOperator = _service.CreateMaintenanceOperator(maintenanceOperator);

    _operatorRepositoryMock.VerifyAll();
    Assert.AreEqual(maintenanceOperator, createdOperator);
  }

}