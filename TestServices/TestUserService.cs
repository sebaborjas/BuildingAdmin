using Domain;
using Services;
using Moq;
using IDataAcess;
using System.Linq.Expressions;

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
    _adminRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Administrator, bool>>>(), It.IsAny<List<string>>()))
      .Returns((Expression<Func<Administrator, bool>> predicate, List<string> includes) => null);
  
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
  [ExpectedException(typeof(ArgumentException))]
  public void CreateAdministratorAlreadyExist()
  {
    _adminRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Administrator, bool>>>(), It.IsAny<List<string>>()))
      .Returns((Expression<Func<Administrator, bool>> predicate, List<string> includes) => new Administrator());

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

  [TestMethod]
  public void TestDeleteManager()
  {
    _managerRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns(new Manager()).Verifiable();
    _managerRepositoryMock.Setup(r => r.Delete(It.IsAny<Manager>())).Verifiable();

    _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object);

    _service.DeleteManager(1);

    _managerRepositoryMock.VerifyAll();
  }

  [TestMethod]
  [ExpectedException(typeof(ArgumentNullException))]
  public void TestDeleteManagerNotFound()
  {
    _managerRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns((Manager)null);
    _managerRepositoryMock.Setup(r => r.Delete(It.IsAny<Manager>())).Throws(new ArgumentNullException());

    _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object);

    _service.DeleteManager(1);

    _managerRepositoryMock.VerifyAll();
  }

}