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
    private Mock<IGenericRepository<CompanyAdministrator>> _companyAdministratorRepositoryMock;
    private Mock<ISessionService> _sessionService;

    [TestInitialize]
    public void SetUp()
    {
        _adminRepositoryMock = new Mock<IGenericRepository<Administrator>>(MockBehavior.Strict);
        _operatorRepositoryMock = new Mock<IGenericRepository<MaintenanceOperator>>(MockBehavior.Strict);
        _managerRepositoryMock = new Mock<IGenericRepository<Manager>>(MockBehavior.Strict);
        _companyAdministratorRepositoryMock = new Mock<IGenericRepository<CompanyAdministrator>>(MockBehavior.Strict);
        _sessionService = new Mock<ISessionService>(MockBehavior.Strict);
    }

    [TestMethod]
    public void CreateCorrectAdministrator()
    {
        _adminRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Administrator, bool>>>(), It.IsAny<List<string>>()))
            .Returns((Expression<Func<Administrator, bool>> predicate, List<string> includes) => null);

        _adminRepositoryMock.Setup(r => r.Insert(It.IsAny<Administrator>())).Verifiable();

        _managerRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Manager, bool>>>(), It.IsAny<List<string>>()))
            .Returns((Expression<Func<Manager, bool>> predicate, List<string> includes) => null);

        _operatorRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<MaintenanceOperator, bool>>>(), It.IsAny<List<string>>()))
            .Returns((Expression<Func<MaintenanceOperator, bool>> predicate, List<string> includes) => null);

        _companyAdministratorRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<CompanyAdministrator, bool>>>(), It.IsAny<List<string>>()))
            .Returns((Expression<Func<CompanyAdministrator, bool>> predicate, List<string> includes) => null);

        _service = new UserService(
            _adminRepositoryMock.Object,
            _operatorRepositoryMock.Object,
            _managerRepositoryMock.Object,
            _sessionService.Object,
            _companyAdministratorRepositoryMock.Object
        );

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

        _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object, _sessionService.Object, _companyAdministratorRepositoryMock.Object);

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
            Apartments = new List<Apartment>(),
            ConstructionCompany = new ConstructionCompany(),
            Expenses = 3000,
            Location = "123,123",
            Tickets = new List<Ticket>()
        };
        var currentUser = new Manager()
        {
            Name = "Administrador",
            LastName = "Administrator",
            Email = "admin@mail.com",
            Id = 1,
            Password = "Pass123.!",
            Buildings = new List<Building> { building }
        };

        _operatorRepositoryMock.Setup(r => r.Insert(It.IsAny<MaintenanceOperator>())).Verifiable();
        _adminRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Administrator, bool>>>(), It.IsAny<List<string>>())).Returns((Administrator)null);
        _managerRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Manager, bool>>>(), It.IsAny<List<string>>())).Returns((Manager)null);
        _operatorRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<MaintenanceOperator, bool>>>(), It.IsAny<List<string>>())).Returns((MaintenanceOperator)null);
        _companyAdministratorRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<CompanyAdministrator, bool>>>(), It.IsAny<List<string>>())).Returns((CompanyAdministrator)null);
        _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(currentUser);

        _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object, _sessionService.Object, _companyAdministratorRepositoryMock.Object);

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
        _companyAdministratorRepositoryMock.VerifyAll();
        Assert.AreEqual(maintenanceOperator, createdOperator);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateCorrectMaintenanceOperatorAlreadyExist()
    {
        _adminRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Administrator, bool>>>(), It.IsAny<List<string>>()))
          .Returns((Expression<Func<Administrator, bool>> predicate, List<string> includes) => new Administrator());

        _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object, _sessionService.Object, _companyAdministratorRepositoryMock.Object);

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

        _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object, _sessionService.Object, _companyAdministratorRepositoryMock.Object);

        _service.DeleteManager(1);

        _managerRepositoryMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestDeleteManagerNotFound()
    {
        _managerRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns((Manager)null);
        _managerRepositoryMock.Setup(r => r.Delete(It.IsAny<Manager>())).Throws(new ArgumentNullException());

        _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object, _sessionService.Object, _companyAdministratorRepositoryMock.Object);

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
            Apartments = new List<Apartment>(),
            ConstructionCompany = new ConstructionCompany(),
            Expenses = 3000,
            Location = "123,123",
            Tickets = new List<Ticket>()
        };
        var currentUser = new Manager()
        {
            Name = "Administrador",
            LastName = "Administrator",
            Email = "admin@mail.com",
            Id = 1,
            Password = "Pass123.!",
            Buildings = new List<Building> { building }
        };

        _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(currentUser);
        _adminRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Administrator, bool>>>(), It.IsAny<List<string>>())).Returns((Administrator)null);
        _managerRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Manager, bool>>>(), It.IsAny<List<string>>())).Returns((Manager)null);
        _operatorRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<MaintenanceOperator, bool>>>(), It.IsAny<List<string>>())).Returns((MaintenanceOperator)null);
        _companyAdministratorRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<CompanyAdministrator, bool>>>(), It.IsAny<List<string>>())).Returns((CompanyAdministrator)null);

        _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object, _sessionService.Object, _companyAdministratorRepositoryMock.Object);

        var maintenanceOperator = new MaintenanceOperator
        {
            Name = "Marc",
            LastName = "Marquez",
            Email = "papa@devalentino.es",
            Password = "Honda.1234",
            Buildings = new List<Building>() { new Building() { Id = 11 } }
        };

        _service.CreateMaintenanceOperator(maintenanceOperator);
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
        _companyAdministratorRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<CompanyAdministrator, bool>>>(), It.IsAny<List<string>>())).Returns((CompanyAdministrator)null);
        _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(currentUser);
        _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object, _sessionService.Object, _companyAdministratorRepositoryMock.Object);

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

    [TestMethod]
    public void TestCreateCompanyAdministrator()
    {
        var currentUser = new CompanyAdministrator
        {
            Name = "Admin",
            LastName = "Admin",
            Email = "admin@test.com",
            Password = "Admin.1234",
            ConstructionCompany = new ConstructionCompany() { Name = "Test Company" }
        };

        _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(currentUser);

        _companyAdministratorRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<CompanyAdministrator, bool>>>(), It.IsAny<List<string>>())).Returns((CompanyAdministrator)null);
        _adminRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Administrator, bool>>>(), It.IsAny<List<string>>())).Returns((Administrator)null);
        _managerRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Manager, bool>>>(), It.IsAny<List<string>>())).Returns((Manager)null);
        _operatorRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<MaintenanceOperator, bool>>>(), It.IsAny<List<string>>())).Returns((MaintenanceOperator)null);

        _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object, _sessionService.Object, _companyAdministratorRepositoryMock.Object);

        var companyAdministrator = new CompanyAdministrator
        {
            Name = "Marc",
            LastName = "Marquez",
            Email = "macr@admin.com",
            Password = "Honda.1234",
        };

        _companyAdministratorRepositoryMock.Setup(r => r.Insert(It.IsAny<CompanyAdministrator>())).Verifiable();

        var createdCompanyAdministrator = _service.CreateCompanyAdministrator(companyAdministrator);

        _companyAdministratorRepositoryMock.Verify(r => r.Insert(It.IsAny<CompanyAdministrator>()), Times.Once);

        Assert.AreEqual(companyAdministrator, createdCompanyAdministrator);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestCreateCompanyAdministratorWithoutCompany()
    {
        var currentUser = new CompanyAdministrator
        {
            Name = "Admin",
            LastName = "Admin",
            Email = "admin@test.com",
            Password = "Admin.1234",
            ConstructionCompany = null
        };

        _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(currentUser);
        _companyAdministratorRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<CompanyAdministrator, bool>>>(), It.IsAny<List<string>>())).Returns((CompanyAdministrator)null);
        _adminRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Administrator, bool>>>(), It.IsAny<List<string>>())).Returns((Administrator)null);
        _managerRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Manager, bool>>>(), It.IsAny<List<string>>())).Returns((Manager)null);
        _operatorRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<MaintenanceOperator, bool>>>(), It.IsAny<List<string>>())).Returns((MaintenanceOperator)null);

        _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object, _sessionService.Object, _companyAdministratorRepositoryMock.Object);

        var companyAdministrator = new CompanyAdministrator
        {
            Name = "Marc",
            LastName = "Marquez",
            Email = "marc@test.com",
            Password = "admin.1234",
        };

        _service.CreateCompanyAdministrator(companyAdministrator);

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestCreateCompanyAdministratorAlreadyExist()
    {
        var currentUser = new CompanyAdministrator
        {
            Name = "Admin",
            LastName = "Admin",
            Email = "admin@test.com",
            Password = "Admin.1234",
            ConstructionCompany = new ConstructionCompany() { Name = "Test Company" }
        };

        _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(currentUser);
        _companyAdministratorRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<CompanyAdministrator, bool>>>(), It.IsAny<List<string>>())).Returns(new CompanyAdministrator());
        _adminRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Administrator, bool>>>(), It.IsAny<List<string>>())).Returns((Administrator)null);
        _managerRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Manager, bool>>>(), It.IsAny<List<string>>())).Returns((Manager)null);
        _operatorRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<MaintenanceOperator, bool>>>(), It.IsAny<List<string>>())).Returns((MaintenanceOperator)null);

        _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object, _sessionService.Object, _companyAdministratorRepositoryMock.Object);

        var companyAdministrator = new CompanyAdministrator
        {
            Name = "Admin",
            LastName = "Admin",
            Email = "admin@test.com",
            Password = "Admin.1234",
            ConstructionCompany = new ConstructionCompany() { Name = "Company" }
        };

        _service.CreateCompanyAdministrator(companyAdministrator);
    }

    [TestMethod]
    public void TestGetManagers()
    {
        var managers = new List<Manager>
        {
            new Manager { Id = 1, Name = "Manager 1", Email = "mail@mail.com" },
            new Manager { Id = 2, Name = "Manager 2", Email = "otroMail@mail.com" }
        };
        _managerRepositoryMock.Setup(r => r.GetAll<Manager>()).Returns(managers);
        _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object, _sessionService.Object, _companyAdministratorRepositoryMock.Object);

        var result = _service.GetManagers();

        CollectionAssert.AreEqual(result, managers);
    }

    [TestMethod]
    public void TestGetCompanyAdministrators()
    {
        var company = new ConstructionCompany
        {
            Name = "Empresa",
            Id = 1
        };
        var currentUser = new CompanyAdministrator
        {
            Id = 1,
            Name = "Admin",
            Email = "admin@mail.com",
            ConstructionCompany = company
        };
        var administrators = new List<CompanyAdministrator>
        {
            new CompanyAdministrator { Id = 2, Name = "Admin", Email = "mail@mail.com", ConstructionCompany = company },
            new CompanyAdministrator { Id = 3, Name = "OtroAdmin", Email = "otroMail@mail.com", ConstructionCompany = company },
            new CompanyAdministrator { Id = 4, Name = "OtroAdminMas", Email = "otroMailMas@mail.com", ConstructionCompany = new ConstructionCompany() }
        };
        _companyAdministratorRepositoryMock.Setup(r => r.GetAll<CompanyAdministrator>()).Returns(administrators);
        _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(currentUser);
        _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object, _sessionService.Object, _companyAdministratorRepositoryMock.Object);

        var result = _service.GetCompanyAdministrators();

        _companyAdministratorRepositoryMock.VerifyAll();
        _sessionService.VerifyAll();

        Assert.AreEqual(result.Count, 2);
        Assert.IsTrue(!result.Any(r => r.ConstructionCompany != company));
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestGetCompanyAdministratorsWhenNoCompany()
    {
        var currentUser = new CompanyAdministrator
        {
            Id = 1,
            Name = "Admin",
            Email = "admin@mail.com",
            ConstructionCompany = null
        };
        _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(currentUser);
        _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object, _sessionService.Object, _companyAdministratorRepositoryMock.Object);

        var result = _service.GetCompanyAdministrators();

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestCreateInvalidAdministrator()
    {
        _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object, _sessionService.Object, _companyAdministratorRepositoryMock.Object);

        _service.CreateAdministrator(null);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestCreateInvalidMaintenanceOperator()
    {
        _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object, _sessionService.Object, _companyAdministratorRepositoryMock.Object);

        _service.CreateMaintenanceOperator(null);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestCreateInvalidCompanyAdministrator()
    {
        _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(new CompanyAdministrator());
        _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object, _sessionService.Object, _companyAdministratorRepositoryMock.Object);

        _service.CreateCompanyAdministrator(null);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestCreateCompanyAdministratorWithInvalidUser()
    {
        _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(new Manager());
        _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object, _sessionService.Object, _companyAdministratorRepositoryMock.Object);

        _service.CreateCompanyAdministrator(null);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void CreateAdministratorError()
    {
        _adminRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Administrator, bool>>>(), It.IsAny<List<string>>()))
            .Returns((Expression<Func<Administrator, bool>> predicate, List<string> includes) => null);

        _adminRepositoryMock.Setup(r => r.Insert(It.IsAny<Administrator>())).Throws(new Exception());

        _managerRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Manager, bool>>>(), It.IsAny<List<string>>()))
            .Returns((Expression<Func<Manager, bool>> predicate, List<string> includes) => null);

        _operatorRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<MaintenanceOperator, bool>>>(), It.IsAny<List<string>>()))
            .Returns((Expression<Func<MaintenanceOperator, bool>> predicate, List<string> includes) => null);

        _companyAdministratorRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<CompanyAdministrator, bool>>>(), It.IsAny<List<string>>()))
            .Returns((Expression<Func<CompanyAdministrator, bool>> predicate, List<string> includes) => null);

        _service = new UserService(
            _adminRepositoryMock.Object,
            _operatorRepositoryMock.Object,
            _managerRepositoryMock.Object,
            _sessionService.Object,
            _companyAdministratorRepositoryMock.Object
        );

        var administrator = new Administrator
        {
            Name = "Fernando",
            LastName = "Alonso",
            Email = "elnano@padre.com",
            Password = "Nano.1234"
        };

        var createdAdmin = _service.CreateAdministrator(administrator);

    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void CreateMaintenanceOperatorError()
    {
        var building = new Building()
        {
            Id = 10,
            Name = "Edificio",
            Address = "Calle, 123, esquina",
            Apartments = new List<Apartment>(),
            ConstructionCompany = new ConstructionCompany(),
            Expenses = 3000,
            Location = "123,123",
            Tickets = new List<Ticket>()
        };
        var currentUser = new Manager()
        {
            Name = "Administrador",
            LastName = "Administrator",
            Email = "admin@mail.com",
            Id = 1,
            Password = "Pass123.!",
            Buildings = new List<Building> { building }
        };

        _operatorRepositoryMock.Setup(r => r.Insert(It.IsAny<MaintenanceOperator>())).Throws(new Exception());
        _adminRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Administrator, bool>>>(), It.IsAny<List<string>>())).Returns((Administrator)null);
        _managerRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Manager, bool>>>(), It.IsAny<List<string>>())).Returns((Manager)null);
        _operatorRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<MaintenanceOperator, bool>>>(), It.IsAny<List<string>>())).Returns((MaintenanceOperator)null);
        _companyAdministratorRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<CompanyAdministrator, bool>>>(), It.IsAny<List<string>>())).Returns((CompanyAdministrator)null);
        _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(currentUser);

        _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object, _sessionService.Object, _companyAdministratorRepositoryMock.Object);

        var maintenanceOperator = new MaintenanceOperator
        {
            Name = "Marc",
            LastName = "Marquez",
            Email = "papa@devalentino.es",
            Password = "Honda.1234",
            Buildings = new List<Building>() { new Building() { Id = 10 } }
        };

        var createdOperator = _service.CreateMaintenanceOperator(maintenanceOperator);

    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestDeleteManagerError()
    {
        _managerRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns(new Manager()).Verifiable();
        _managerRepositoryMock.Setup(r => r.Delete(It.IsAny<Manager>())).Throws(new Exception());

        _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object, _sessionService.Object, _companyAdministratorRepositoryMock.Object);

        _service.DeleteManager(1);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestCreateCompanyAdministratorError()
    {
        var currentUser = new CompanyAdministrator
        {
            Name = "Admin",
            LastName = "Admin",
            Email = "admin@test.com",
            Password = "Admin.1234",
            ConstructionCompany = new ConstructionCompany() { Name = "Test Company" }
        };

        _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(currentUser);

        _companyAdministratorRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<CompanyAdministrator, bool>>>(), It.IsAny<List<string>>())).Returns((CompanyAdministrator)null);
        _adminRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Administrator, bool>>>(), It.IsAny<List<string>>())).Returns((Administrator)null);
        _managerRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Manager, bool>>>(), It.IsAny<List<string>>())).Returns((Manager)null);
        _operatorRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<MaintenanceOperator, bool>>>(), It.IsAny<List<string>>())).Returns((MaintenanceOperator)null);

        _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object, _sessionService.Object, _companyAdministratorRepositoryMock.Object);

        var companyAdministrator = new CompanyAdministrator
        {
            Name = "Marc",
            LastName = "Marquez",
            Email = "macr@admin.com",
            Password = "Honda.1234",
        };

        _companyAdministratorRepositoryMock.Setup(r => r.Insert(It.IsAny<CompanyAdministrator>())).Throws(new Exception());

        var createdCompanyAdministrator = _service.CreateCompanyAdministrator(companyAdministrator);

    }

    [TestMethod]
    public void TestGetMaintenanceOperators_CurrentUserIsManager_ReturnsOperatorsForBuildings()
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
            Name = "Manager",
            Email = "manager@test.com",
            Buildings = [building1, building2]
};
        var operators = new List<MaintenanceOperator>
        {
                new MaintenanceOperator { Id = 1, Name = "Operator", Email = "test@test.com", LastName = "Test", Buildings = new List<Building> { building1 } }
        };
        _operatorRepositoryMock.Setup(r => r.GetAll<MaintenanceOperator>()).Returns(operators);
        _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(currentUser);
        _service = new UserService(_adminRepositoryMock.Object, _operatorRepositoryMock.Object, _managerRepositoryMock.Object, _sessionService.Object, _companyAdministratorRepositoryMock.Object);

    }

    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void TestGetMaintenanceOperators_CurrentUserIsNotManager_ThrowsException()
    {
        var currentUser = new Administrator();

        _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(currentUser);

        _service.GetMaintenanceOperators();

    }

    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void TestGetMaintenanceOperators_NoBuildings_ThrowsException()
    {
        var currentUser = new Manager()
        {
            Buildings = new List<Building>()
        };

        _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(currentUser);

        _service.GetMaintenanceOperators();

    }

    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void TestGetMaintenanceOperators_NoOperators_ThrowsException()
    {
        var currentUser = new Manager()
        {
            Buildings = new List<Building>()
            {
                new Building() { Id = 1 },
                new Building() { Id = 2 }
            }
        };

        _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(currentUser);

        _operatorRepositoryMock.Setup(x => x.GetAll<MaintenanceOperator>()).Returns(new List<MaintenanceOperator>());

        _service.GetMaintenanceOperators();

    }

}