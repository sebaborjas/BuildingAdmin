using Domain;
using Services;
using Moq;
using IDataAccess;
using System.Linq.Expressions;
using IServices;

namespace TestServices;

[TestClass]
public class TestUserService
{
    private UserService _service;
    private Mock<IGenericRepository<Administrator>> _adminRepositoryMock;
    private Mock<IGenericRepository<MaintenanceOperator>> _operatorRepositoryMock;
    private Mock<IGenericRepository<Manager>> _managerRepositoryMock;
    private Mock<ISessionService> _sessionService;



    [TestInitialize]
    public void SetUp()
    {
        _adminRepositoryMock = new Mock<IGenericRepository<Administrator>>(MockBehavior.Strict);
        _operatorRepositoryMock = new Mock<IGenericRepository<MaintenanceOperator>>();
        _managerRepositoryMock = new Mock<IGenericRepository<Manager>>();
        _sessionService = new Mock<ISessionService>();
    }
    
    [TestMethod]
    public void CreateCorrectAdministrator()
    {
        _adminRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Administrator, bool>>>(), It.IsAny<List<string>>()))
          .Returns((Expression<Func<Administrator, bool>> predicate, List<string> includes) => null);

        _adminRepositoryMock.Setup(r => r.Insert(It.IsAny<Administrator>())).Verifiable();

        _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object, _sessionService.Object);

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


        _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object, _sessionService.Object);

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
        var building = new Building()
        {
            Id = 10,
            Name = "Edificio",
            Address = "Calle, 123, esquina",
            Apartments = [],
            ConstructionCompany = new ConstructionCompany(),
            Expenses = 3000,
            Location = "123,123",
            Tickets = []
        };
        var currentUser = new Manager()
        {
            Name = "Administrador",
            LastName = "Administrator",
            Email = "admin@mail.com",
            Id = 1,
            Password = "Pass123.!",
            Buildings = [building]
        };
        _operatorRepositoryMock.Setup(r => r.Insert(It.IsAny<MaintenanceOperator>())).Verifiable();
        _adminRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Administrator, bool>>>(), It.IsAny<List<string>>())).Returns((Administrator)null);
        _managerRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Manager, bool>>>(), It.IsAny<List<string>>())).Returns((Manager)null);
        _operatorRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<MaintenanceOperator, bool>>>(), It.IsAny<List<string>>())).Returns((MaintenanceOperator)null);
        _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(currentUser);
        _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object, _sessionService.Object);

        var maintenanceOperator = new MaintenanceOperator
        {
            Name = "Marc",
            LastName = "Marquez",
            Email = "papa@devalentino.es",
            Password = "Honda.1234",
            Buildings = new List<Building>() { new Building() { Id = 10 } }
        };

        var createdOperator = _service.CreateMaintenanceOperator(maintenanceOperator);

        _sessionService.VerifyAll();
        _operatorRepositoryMock.VerifyAll();
        _adminRepositoryMock.VerifyAll();
        _managerRepositoryMock.VerifyAll();
        Assert.AreEqual(maintenanceOperator, createdOperator);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateCorrectMaintenanceOperatorAlreadyExist()
    {
        _adminRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Administrator, bool>>>(), It.IsAny<List<string>>()))
          .Returns((Expression<Func<Administrator, bool>> predicate, List<string> includes) => new Administrator());

        _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object, _sessionService.Object);

        var maintenanceOperator = new MaintenanceOperator
        {
            Name = "Valentino",
            LastName = "Rossi",
            Email = "yamaha@goat.es",
            Password = "Yamaha.1234",
        };

        var createdOperator = _service.CreateMaintenanceOperator(maintenanceOperator);

        _operatorRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void TestDeleteManager()
    {
        _managerRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns(new Manager()).Verifiable();
        _managerRepositoryMock.Setup(r => r.Delete(It.IsAny<Manager>())).Verifiable();

        _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object, _sessionService.Object);

        _service.DeleteManager(1);

        _managerRepositoryMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestDeleteManagerNotFound()
    {
        _managerRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns((Manager)null);
        _managerRepositoryMock.Setup(r => r.Delete(It.IsAny<Manager>())).Throws(new ArgumentNullException());

        _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object, _sessionService.Object);

        _service.DeleteManager(1);

        _managerRepositoryMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestCreateMaintenanceOperatorForInvalidBuilding()
    {
        var building = new Building()
        {
            Id = 10,
            Name = "Edificio",
            Address = "Calle, 123, esquina",
            Apartments = [],
            ConstructionCompany = new ConstructionCompany(),
            Expenses = 3000,
            Location = "123,123",
            Tickets = []
        };
        var currentUser = new Manager()
        {
            Name = "Administrador",
            LastName = "Administrator",
            Email = "admin@mail.com",
            Id = 1,
            Password = "Pass123.!",
            Buildings = [building]
        };
        _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(currentUser);
        _adminRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Administrator, bool>>>(), It.IsAny<List<string>>())).Returns((Administrator)null);
        _managerRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Manager, bool>>>(), It.IsAny<List<string>>())).Returns((Manager)null);
        _operatorRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<MaintenanceOperator, bool>>>(), It.IsAny<List<string>>())).Returns((MaintenanceOperator)null);
        _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object, _sessionService.Object);

        var maintenanceOperator = new MaintenanceOperator
        {
            Name = "Marc",
            LastName = "Marquez",
            Email = "papa@devalentino.es",
            Password = "Honda.1234",
            Buildings = new List<Building>() { new Building() { Id = 11 } }
        };

        var createdOperator = _service.CreateMaintenanceOperator(maintenanceOperator);
    }
    
    [TestMethod]
    public void CreateMaintenanceOperatorWithMoreBuildings()
    {
        var building1 = new Building()
        {
            Id = 10,
            Name = "Edificio Uno",
            Address = "Calle, 123, esquina",
            Apartments = [],
            ConstructionCompany = new ConstructionCompany(),
            Expenses = 3000,
            Location = "123,123",
            Tickets = []
        };
        var building2 = new Building()
        {
            Id = 11,
            Name = "Edificio Dos",
            Address = "Calle, 321, esquina",
            Apartments = [],
            ConstructionCompany = new ConstructionCompany(),
            Expenses = 5000,
            Location = "321,321",
            Tickets = []
        };
        var currentUser = new Manager()
        {
            Name = "Administrador",
            LastName = "Administrator",
            Email = "admin@mail.com",
            Id = 1,
            Password = "Pass123.!",
            Buildings = [building1, building2]
        };
        _operatorRepositoryMock.Setup(r => r.Insert(It.IsAny<MaintenanceOperator>())).Verifiable();
        _adminRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Administrator, bool>>>(), It.IsAny<List<string>>())).Returns((Administrator)null);
        _managerRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Manager, bool>>>(), It.IsAny<List<string>>())).Returns((Manager)null);
        _operatorRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<MaintenanceOperator, bool>>>(), It.IsAny<List<string>>())).Returns((MaintenanceOperator)null);
        _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(currentUser);
        _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object, _sessionService.Object);

        var maintenanceOperator = new MaintenanceOperator
        {
            Name = "Marc",
            LastName = "Marquez",
            Email = "papa@devalentino.es",
            Password = "Honda.1234",
            Buildings = new List<Building>() { new Building() { Id = 10 }, new Building() { Id = 11 } }
        };

        var createdOperator = _service.CreateMaintenanceOperator(maintenanceOperator);

        _sessionService.VerifyAll();
        _operatorRepositoryMock.VerifyAll();
        _adminRepositoryMock.VerifyAll();
        _managerRepositoryMock.VerifyAll();
        Assert.AreEqual(maintenanceOperator, createdOperator);
        CollectionAssert.Contains(maintenanceOperator.Buildings, building1);
        CollectionAssert.Contains(maintenanceOperator.Buildings, building2);
    }


}