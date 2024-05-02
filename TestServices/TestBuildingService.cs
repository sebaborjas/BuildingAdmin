using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services;
using IDataAccess;
using Domain;
using System.Linq.Expressions;
using IServices;

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

        private Manager _user;
        private Building _building;
        private Apartment _apartment;

        [TestInitialize]
        public void SetUp()
        {
            _buildingRepositoryMock = new Mock<IGenericRepository<Building>>(MockBehavior.Strict);

            _ownerRepositoryMock = new Mock<IGenericRepository<Owner>>(MockBehavior.Strict);
            _sessionServiceMock = new Mock<ISessionService>(MockBehavior.Strict);
            _constructionCompanyMock = new Mock<IGenericRepository<ConstructionCompany>>(MockBehavior.Strict);
            _managerRepositoryMock = new Mock<IGenericRepository<Manager>>(MockBehavior.Strict);

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
                Apartments = [_apartment],
                Address = "Rivera, 1111, Soca",
                ConstructionCompany = new ConstructionCompany(),
                Expenses = 2000,
                Location = "111,111",
                Name = "Edificio Las Delicias",
                Tickets = []
            };

            _user = new Manager()
            {
                Buildings = [_building],
                Email = "manager@correo.com",
                Id = 1,
                Name = "Lucas",
                LastName = "Gonzalez",
                Password = "Lu.Go123!",
            };

        }

        [TestMethod]
        public void CreateBuilding()
        {
            _buildingRepositoryMock.Setup(r => r.Insert(It.IsAny<Building>())).Verifiable();

            _buildingRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Building, bool>>>(), It.IsAny<List<string>>()))
              .Returns((Expression<Func<Building, bool>> predicate, List<string> includes) => null);
            _constructionCompanyMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<ConstructionCompany, bool>>>(), It.IsAny<List<string>>())).Returns((ConstructionCompany)null);
            _constructionCompanyMock.Setup(r => r.Insert(It.IsAny<ConstructionCompany>())).Verifiable();
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);
            _managerRepositoryMock.Setup(r => r.Update(It.IsAny<Manager>())).Verifiable();

            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);

            var building = new Building
            {
                Name = "Edificio nuevo",
                Address = "Calle, 123, esquina",
                Location = "111,111",
                ConstructionCompany = new ConstructionCompany(),
                Expenses = 1000,
                Apartments = new List<Apartment>()
            };

            var result = _buildingService.CreateBuilding(building);

            _buildingRepositoryMock.VerifyAll();
            _constructionCompanyMock.VerifyAll();
            _sessionServiceMock.VerifyAll();
            _managerRepositoryMock.VerifyAll();
            Assert.AreEqual(building, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateBuildingAlreadyExists()
        {
            _buildingRepositoryMock.Setup(r => r.Insert(It.IsAny<Building>())).Verifiable();

            _buildingRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Building, bool>>>(), It.IsAny<List<string>>()))
              .Returns((Expression<Func<Building, bool>> predicate, List<string> includes) => new Building());

            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);

            var building = new Building
            {
                Name = "Edificio nuevo",
                Address = "Calle, 123, esquina",
                Location = "111,111",
                ConstructionCompany = new ConstructionCompany(),
                Expenses = 1000,
                Apartments = new List<Apartment>()
            };

            var result = _buildingService.CreateBuilding(building);

            _buildingRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void DeleteBuilding()
        {
            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);
            _buildingRepositoryMock.Setup(r => r.Delete(It.IsAny<Building>())).Verifiable();

            _buildingService.DeleteBuilding(1);

            _sessionServiceMock.VerifyAll();
            _buildingRepositoryMock.Verify(r => r.Delete(It.IsAny<Building>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteBuildingThatNotExist()
        {
            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);
            _buildingRepositoryMock.Setup(r => r.Delete(It.IsAny<Building>())).Verifiable();

            _buildingService.DeleteBuilding(3);

            _sessionServiceMock.VerifyAll();
            _buildingRepositoryMock.Verify(r => r.Delete(It.IsAny<Building>()), Times.Once);
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
            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);
            _buildingRepositoryMock.Setup(r => r.Update(It.IsAny<Building>())).Verifiable();
            _ownerRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Owner, bool>>>(), It.IsAny<List<string>>())).Returns((Owner)null);
            _ownerRepositoryMock.Setup(r => r.Insert(It.IsAny<Owner>())).Verifiable();
            _constructionCompanyMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<ConstructionCompany, bool>>>(), It.IsAny<List<string>>())).Returns((ConstructionCompany)null);
            _constructionCompanyMock.Setup(r => r.Insert(It.IsAny<ConstructionCompany>())).Verifiable();
            var modifiedBuilding = new Building();
            modifiedBuilding.Expenses = 5000;
            modifiedBuilding.ConstructionCompany = new ConstructionCompany() { Id = 2, Name = "Constructora modificada" };
            modifiedBuilding.Apartments.Add(new Apartment()
            {
                Owner = new Owner() { Email = "otroOwner@mail.com", Name = "Dueño", LastName = "Nuevo " },
                Id = 1
            });

            var result = _buildingService.ModifyBuilding(1, modifiedBuilding);

            Assert.AreEqual(modifiedBuilding.Expenses, result.Expenses);
            Assert.AreEqual(modifiedBuilding.ConstructionCompany.Name, result.ConstructionCompany.Name);
            Assert.AreEqual(modifiedBuilding.Apartments.First().Owner, result.Apartments.First().Owner);
            _buildingRepositoryMock.VerifyAll();
            _sessionServiceMock.VerifyAll();
        }

        [TestMethod]
        public void ModifyBuildingWithExistingOwner()
        {
            var existingOwner = new Owner()
            {
                Id = 5,
                Email = "ownerViejo@mail.com",
                Name = "Dueño",
                LastName = "Owner"
            };
            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);
            _buildingRepositoryMock.Setup(r => r.Update(It.IsAny<Building>())).Verifiable();
            _ownerRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Owner, bool>>>(), It.IsAny<List<string>>())).Returns(existingOwner);
            _constructionCompanyMock.Setup(r=>r.GetByCondition(It.IsAny<Expression<Func<ConstructionCompany, bool>>>(), It.IsAny<List<string>>())).Returns((ConstructionCompany)null);
            _constructionCompanyMock.Setup(r => r.Insert(It.IsAny<ConstructionCompany>())).Verifiable();
            var modifiedBuilding = new Building();
            modifiedBuilding.Expenses = 5000;
            modifiedBuilding.ConstructionCompany = new ConstructionCompany() { Id = 2, Name = "Constructora modificada" };
            modifiedBuilding.Apartments.Add(new Apartment()
            {
                Owner = new Owner() { Email = "ownerViejo@mail.com", Name = "Dueño", LastName = "Nuevo " },
                Id = 1
            });

            var result = _buildingService.ModifyBuilding(1, modifiedBuilding);

            _buildingRepositoryMock.VerifyAll();
            _sessionServiceMock.VerifyAll();
            Assert.AreEqual(modifiedBuilding.Expenses, result.Expenses);
            Assert.AreEqual(modifiedBuilding.ConstructionCompany.Name, result.ConstructionCompany.Name);
            Assert.AreEqual(existingOwner, result.Apartments.First().Owner);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ModifyBuildingThatNotExist()
        {
            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);
            _buildingRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns((Building)null);
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
        public void GetBuilding()
        {
            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);

            var result = _buildingService.Get(1);

            _sessionServiceMock.VerifyAll();
            Assert.AreEqual(result, _building);

        }

        [TestMethod]
        public void GetAllBuildingsForUser()
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
            _user.Buildings = [_building, otherBuilding];
            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);

            var result = _buildingService.GetAllBuildingsForUser();

            _sessionServiceMock.VerifyAll();
            Assert.AreEqual(_user.Buildings, result);

        }
    }
}