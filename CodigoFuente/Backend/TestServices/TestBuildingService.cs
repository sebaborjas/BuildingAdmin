using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services;
using IDataAccess;
using Domain;
using System.Linq.Expressions;
using IServices;
using System.Text.RegularExpressions;

namespace TestServices
{
    [TestClass]
    public class TestBuildingService
    {
        BuildingService _buildingService;

        private Mock<ISessionService> _sessionServiceMock;
        private Mock<IGenericRepository<Building>> _buildingRepositoryMock;
        private Mock<IGenericRepository<Owner>> _ownerRepositoryMock;
        private Mock<IGenericRepository<Manager>> _managerRepositoryMock;
        private Mock<IGenericRepository<ConstructionCompany>> _constructionCompanyMock;

        private Building _building;
        private Apartment _apartment;
        private CompanyAdministrator _companyAdministrator;
        private Manager _manager;
        private ConstructionCompany _constructionCompany;

        [TestInitialize]
        public void SetUp()
        {
            _buildingRepositoryMock = new Mock<IGenericRepository<Building>>(MockBehavior.Strict);

            _ownerRepositoryMock = new Mock<IGenericRepository<Owner>>(MockBehavior.Strict);
            _sessionServiceMock = new Mock<ISessionService>(MockBehavior.Strict);
            _constructionCompanyMock = new Mock<IGenericRepository<ConstructionCompany>>(MockBehavior.Strict);
            _managerRepositoryMock = new Mock<IGenericRepository<Manager>>(MockBehavior.Strict);

            _constructionCompany = new ConstructionCompany()
            {
                Id = 1,
                Name = "Test Construction Company"
            };

            _apartment = new Apartment()
            {
                Id = 1,
                Bathrooms = 2,
                DoorNumber = 3,
                Floor = 1,
                HasTerrace = false,
                Owner = new Owner() { Id = 1, Email = "owner@mail.com", Name = "Nombre", LastName = "Apellido" },
                Rooms = 2
            };

            _building = new Building()
            {
                Id = 1,
                Apartments = new List<Apartment> { _apartment },
                Address = "Rivera, 1111, Soca",
                ConstructionCompany = _constructionCompany,
                Expenses = 2000,
                Location = "111,111",
                Name = "Edificio Las Delicias",
                Tickets = []
            };

            _companyAdministrator = new CompanyAdministrator()
            {
                Email = "company@admin.com",
                Name = "Admin",
                LastName = "Company",
                Password = "Test.1234!",
                ConstructionCompany = _constructionCompany
            };

            _manager = new Manager
            {
                Name = "Manager",
                LastName = "Manager",
                Email = "test@mail.com",
                Password = "Test.1234!",
                Buildings = new List<Building> { _building }
            };

        }

        [TestMethod]
        public void CreateBuilding()
        {
            _buildingRepositoryMock.Setup(r => r.Insert(It.IsAny<Building>())).Verifiable();
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_companyAdministrator);
            _buildingRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Building, bool>>>(), It.IsAny<List<string>>())).Returns((Building)null);
            _ownerRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Owner, bool>>>(), It.IsAny<List<string>>())).Returns(_apartment.Owner);
            _managerRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Manager, bool>>>(), It.IsAny<List<string>>())).Returns(_manager);
            _managerRepositoryMock.Setup(r => r.Update(It.IsAny<Manager>())).Verifiable();

            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);

            var building = new Building
            {
                Name = "Edificio nuevo",
                Address = "Calle, 123, esquina",
                Location = "111,111",
                ConstructionCompany = new ConstructionCompany() { Name = "Construction" },
                Expenses = 1000,
                Apartments = new List<Apartment> { _apartment }
            };

            var result = _buildingService.CreateBuilding(building, "manager@mail.com");

            _buildingRepositoryMock.VerifyAll();
            _sessionServiceMock.VerifyAll();
            _ownerRepositoryMock.VerifyAll();
            _managerRepositoryMock.VerifyAll();

            Assert.AreEqual(building, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateBuildingAlreadyExists()
        {
            _buildingRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Building, bool>>>(), It.IsAny<List<string>>())).Returns(_building);
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_companyAdministrator);

            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);

            var building = new Building
            {
                Name = "Edificio Las Delicias",
                Address = "Rivera, 1111, Soca",
                Location = "111,111",
                ConstructionCompany = new ConstructionCompany() { Name = "Construction" },
                Expenses = 1000,
                Apartments = new List<Apartment> { _apartment }
            };

            var result = _buildingService.CreateBuilding(building);
        }

        [TestMethod]
        public void DeleteBuilding()
        {
            _buildingRepositoryMock.Setup(r => r.Delete(It.IsAny<Building>())).Verifiable();
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_companyAdministrator);
            _buildingRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Building, bool>>>(), It.IsAny<List<string>>())).Returns(_building);

            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);

            _buildingService.DeleteBuilding(1);

            _buildingRepositoryMock.VerifyAll();
            _sessionServiceMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteBuildingThatNotExist()
        {
            _buildingRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Building, bool>>>(), It.IsAny<List<string>>())).Returns((Building)null);
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_companyAdministrator);

            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);

            _buildingService.DeleteBuilding(1);

            _buildingRepositoryMock.VerifyAll();
            _sessionServiceMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestDeleteBuildingNotManager()
        {
            var user = new Administrator();
            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(user);

            _buildingService.DeleteBuilding(1);

            _buildingRepositoryMock.VerifyAll();
        }


        [TestMethod]
        public void ModifyBuilding()
        {
            var existingBuilding = new Building()
            {
                Id = 1,
                Address = "Rivera, 1111, Soca",
                ConstructionCompany = new ConstructionCompany() { Name = "Constructora" },
                Expenses = 2000,
                Location = "111,111",
                Name = "Edificio Las Delicias",
                Apartments = new List<Apartment>() { _apartment }
            };

            var modifiedBuilding = new Building()
            {
                Id = existingBuilding.Id,
                Address = existingBuilding.Address,
                ConstructionCompany = existingBuilding.ConstructionCompany,
                Expenses = 1000,
                Location = existingBuilding.Location,
                Name = existingBuilding.Name,
                Apartments = new List<Apartment>()
                {
                    new Apartment()
                    {
                        Owner = new Owner() { Email = "newOwner@test.com", Name = "Nuevo", LastName = "Owner" },
                        Id = 1
                    }
                }
            };

            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_companyAdministrator);
            _buildingRepositoryMock.Setup(r => r.Update(It.IsAny<Building>())).Verifiable();
            _ownerRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Owner, bool>>>(), It.IsAny<List<string>>())).Returns(_apartment.Owner);
            _constructionCompanyMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<ConstructionCompany, bool>>>(), It.IsAny<List<string>>())).Returns(existingBuilding.ConstructionCompany);
            _buildingRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Building, bool>>>(), It.IsAny<List<string>>())).Returns(existingBuilding);

            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);

            var result = _buildingService.ModifyBuilding(1, modifiedBuilding);

            Assert.AreEqual(modifiedBuilding, result);
            _buildingRepositoryMock.Verify(r => r.Update(It.IsAny<Building>()), Times.Once);
        }

        [TestMethod]
        public void ModifyBuildingWithExistingOwner()
        {
            var existingOwner = new Owner()
            {
                Id = 5,
                Email = "ownerViejo@mail.com",
                Name = "Last",
                LastName = "Owner"
            };

            var existingBuilding = new Building()
            {
                Id = 1,
                Address = "Rivera, 1111, Soca",
                ConstructionCompany = new ConstructionCompany() { Name = "Constructora" },
                Expenses = 2000,
                Location = "111,111",
                Name = "Edificio Las Delicias",
                Apartments = new List<Apartment>()
                {
                    new Apartment()
                    {
                        Owner = existingOwner,
                        Id = 1
                    }
                }
            };

            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_companyAdministrator);
            _buildingRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Building, bool>>>(), It.IsAny<List<string>>())).Returns(existingBuilding);
            _buildingRepositoryMock.Setup(r => r.Update(It.IsAny<Building>())).Verifiable();
            _ownerRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Owner, bool>>>(), It.IsAny<List<string>>())).Returns(existingOwner);

            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);

            var modifiedBuilding = new Building()
            {
                Id = existingBuilding.Id,
                Address = existingBuilding.Address,
                ConstructionCompany = existingBuilding.ConstructionCompany,
                Expenses = existingBuilding.Expenses,
                Location = existingBuilding.Location,
                Name = existingBuilding.Name,
                Apartments = new List<Apartment>()
                {
                    new Apartment()
                    {
                        Owner = new Owner() { Email = "ownerViejo@mail.com", Name = "Due�o", LastName = "Nuevo" },
                        Id = 1
                    }
                }
            };

            var result = _buildingService.ModifyBuilding(existingBuilding.Id, modifiedBuilding);

            _buildingRepositoryMock.VerifyAll();
            _sessionServiceMock.VerifyAll();
            Assert.AreEqual(existingOwner.Email, result.Apartments.First().Owner.Email);
            Assert.AreEqual(existingOwner.Name, result.Apartments.First().Owner.Name);
            Assert.AreEqual(existingOwner.LastName, result.Apartments.First().Owner.LastName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ModifyBuildingThatNotExist()
        {
            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_companyAdministrator);
            _buildingRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Building, bool>>>(), It.IsAny<List<string>>())).Returns((Building)null);
            _buildingRepositoryMock.Setup(r => r.Update(It.IsAny<Building>())).Verifiable();
            var modifiedBuilding = new Building();

            _buildingService.ModifyBuilding(3, modifiedBuilding);

            _buildingRepositoryMock.VerifyAll();
            _sessionServiceMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestModifyBuildingNotManager()
        {
            var user = new Administrator();
            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(user);

            var modifiedBuilding = new Building();

            _buildingService.ModifyBuilding(1, modifiedBuilding);
        }

        [TestMethod]
        public void GetBuildingForCompanyAdminById()
        {
            _buildingRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Building, bool>>>(), It.IsAny<List<string>>())).Returns(_building);
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_companyAdministrator);

            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);

            var result = _buildingService.Get(1);

            _buildingRepositoryMock.VerifyAll();
            _sessionServiceMock.VerifyAll();

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void GetBuildingForCompanyAdmin()
        {
            var building = new Building()
            {
                Id = 15,
                Address = "OtraCalle, 321, esquina",
                ConstructionCompany = new ConstructionCompany() { Name = "Constructora" },
                Expenses = 2000,
                Location = "12345,12345",
                Name = "Nuevo edificio",
            };

            var companyAdmin = new CompanyAdministrator()
            {
                Email = "company@admin.com",
                Name = "Admin",
                LastName = "Company",
                Password = "Test.1234!",
                ConstructionCompany = new ConstructionCompany() { Name = "Constructora" }
            };
            _buildingRepositoryMock.Setup(_buildingRepositoryMock => _buildingRepositoryMock.GetByCondition(It.IsAny<Expression<Func<Building, bool>>>(), It.IsAny<List<string>>())).Returns(building);
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(companyAdmin);

            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);

            var result = _buildingService.Get(null);

            _buildingRepositoryMock.VerifyAll();
            _sessionServiceMock.VerifyAll();
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void GetAllBuildingsForAdmin()
        {
            var otherBuilding = new Building()
            {
                Id = 15,
                Address = "OtraCalle, 321, esquina",
                ConstructionCompany = new ConstructionCompany(),
                Expenses = 2000,
                Location = "12345,12345",
                Name = "Nuevo edificio",
            };
            _buildingRepositoryMock.Setup(r => r.GetAll<Building>()).Returns(new List<Building> { _building, otherBuilding });
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(new Administrator());

            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);

            var result = _buildingService.Get(null);

            _sessionServiceMock.VerifyAll();
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetAllBuildingsForManager()
        {
            var otherBuilding = new Building()
            {
                Id = 15,
                Address = "OtraCalle, 321, esquina",
                ConstructionCompany = new ConstructionCompany(),
                Expenses = 2000,
                Location = "12345,12345",
                Name = "Nuevo edificio",
            };
            _manager.Buildings = new List<Building> { _building, otherBuilding };
            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_manager);

            var result = _buildingService.Get(null);

            _sessionServiceMock.VerifyAll();
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetAllBuildingsForManagerById()
        {
            var otherBuilding = new Building()
            {
                Id = 15,
                Address = "OtraCalle, 321, esquina",
                ConstructionCompany = new ConstructionCompany(),
                Expenses = 2000,
                Location = "12345,12345",
                Name = "Nuevo edificio",
            };
            _manager.Buildings = [_building, otherBuilding];
            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_manager);

            var result = _buildingService.Get(15);

            _sessionServiceMock.VerifyAll();
            Assert.AreEqual(1, result.Count);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGetBuildingNotAuthorized()
        {
            var user = new MaintenanceOperator();
            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(user);

            _buildingService.Get(1);
        }

        [TestMethod]
        public void GetManagerName()
        {
            _managerRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Manager, bool>>>(), It.IsAny<List<string>>())).Returns(_manager);
            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);

            var result = _buildingService.GetManagerName(1);

            _managerRepositoryMock.VerifyAll();
            Assert.AreEqual(_manager.Name, result);
        }

        [TestMethod]
        public void GetManagerNameNotFound()
        {
            _managerRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Manager, bool>>>(), It.IsAny<List<string>>())).Returns((Manager)null);
            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);

            var result = _buildingService.GetManagerName(1);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestChangeBuildingManager()
        {
            _building = new Building
            {
                Id = 1,
                Name = "Building A",
                Address = "Calle, 1234, esquina",
                Location = "111, 222",
                Expenses = 1000,
                ConstructionCompany = new ConstructionCompany { Name = "Company" },
                Apartments = new List<Apartment>()
            };

            var oldManager = new Manager
            {
                Id = 1,
                Name = "Old manager",
                Email = "old@mail.com",
                Password = "Test.1234!",
                Buildings = new List<Building> { _building }
            };

            var newManager = new Manager
            {
                Id = 2,
                Name = "New manager",
                Email = "new@mail.com",
                Password = "Test.1234!",
                Buildings = new List<Building>()
            };

            _buildingRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Building, bool>>>(), It.IsAny<List<string>>()))
                .Returns(_building);

            _managerRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Manager, bool>>>(), It.IsAny<List<string>>()))
                .Returns((Expression<Func<Manager, bool>> predicate, List<string> includes) =>
                {
                    var compiledPredicate = predicate.Compile();
                    if (compiledPredicate(oldManager)) return oldManager;
                    if (compiledPredicate(newManager)) return newManager;
                    return null;
                });

            _managerRepositoryMock.Setup(r => r.Update(It.IsAny<Manager>())).Verifiable();
            _buildingRepositoryMock.Setup(r => r.Update(It.IsAny<Building>())).Verifiable();

            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_companyAdministrator);

            _buildingService = new BuildingService(
                _buildingRepositoryMock.Object,
                _sessionServiceMock.Object,
                _ownerRepositoryMock.Object,
                _managerRepositoryMock.Object,
                _constructionCompanyMock.Object
            );

            _buildingService.ChangeBuildingManager(_building.Id, newManager.Id);

            _buildingRepositoryMock.Verify(r => r.Update(It.IsAny<Building>()), Times.Never);
            _managerRepositoryMock.Verify(r => r.Update(It.Is<Manager>(m => m.Id == oldManager.Id)), Times.Once);
            _managerRepositoryMock.Verify(r => r.Update(It.Is<Manager>(m => m.Id == newManager.Id)), Times.Once);
            _sessionServiceMock.VerifyAll();

            var managerName = _buildingService.GetManagerName(_building.Id);
            Assert.AreEqual(newManager.Name, managerName);

            Assert.IsFalse(oldManager.Buildings.Contains(_building));
            Assert.IsTrue(newManager.Buildings.Contains(_building));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestChangeBuildingManagerBuildingNotFound()
        {
            _buildingRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Building, bool>>>(), It.IsAny<List<string>>()))
                .Returns((Building)null);

            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_companyAdministrator);

            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);

            _buildingService.ChangeBuildingManager(1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestChangeBuildingManagerOldManagerNotFound()
        {
            _building = new Building
            {
                Id = 1,
                Name = "Building A",
                Address = "Calle, 1234, esquina",
                Location = "111, 222",
                Expenses = 1000,
                ConstructionCompany = new ConstructionCompany { Name = "Company" },
                Apartments = new List<Apartment>()
            };

            var newManager = new Manager
            {
                Id = 2,
                Name = "New manager",
                Email = "test@mail.com",
                Buildings = new List<Building>()
            };

            _buildingRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Building, bool>>>(), It.IsAny<List<string>>()))
                .Returns(_building);

            _managerRepositoryMock.SetupSequence(r => r.GetByCondition(It.IsAny<Expression<Func<Manager, bool>>>(), It.IsAny<List<string>>()))
                .Returns((Manager)null)
                .Returns(newManager);

            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_companyAdministrator);

            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);

            _buildingService.ChangeBuildingManager(_building.Id, newManager.Id);

            _buildingRepositoryMock.VerifyAll();
            _sessionServiceMock.VerifyAll();
            _managerRepositoryMock.VerifyAll();
            _constructionCompanyMock.VerifyAll();

            _managerRepositoryMock.VerifyAll();
        }
    }
}