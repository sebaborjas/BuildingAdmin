using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services;
using IDataAccess;
using Domain;
using IDataAcess;
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

        private Manager _user;
        private Building _building;
        private Apartment _apartment;

        [TestInitialize]
        public void SetUp()
        {
            _buildingRepositoryMock = new Mock<IGenericRepository<Building>>(MockBehavior.Strict);

            _buildingRepositoryMock = new Mock<IGenericRepository<Building>>(MockBehavior.Strict);
            _sessionServiceMock = new Mock<ISessionService>(MockBehavior.Strict);

            _apartment = new Apartment()
            {
                Id = 1,
                Bathrooms = 2,
                DoorNumber = 3,
                Floor = 1,
                HasTerrace = false,
                Owner = new Owner(),
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

            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object);

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
            Assert.AreEqual(building, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateBuildingAlreadyExists()
        {
            _buildingRepositoryMock.Setup(r => r.Insert(It.IsAny<Building>())).Verifiable();

            _buildingRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Building, bool>>>(), It.IsAny<List<string>>()))
              .Returns((Expression<Func<Building, bool>> predicate, List<string> includes) => new Building());

            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object);

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
            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object);

            _buildingRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns(_building);

            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);

            _buildingRepositoryMock.Setup(r => r.Delete(It.IsAny<Building>())).Verifiable();

            _buildingService.DeleteBuilding(1);

            _buildingRepositoryMock.Verify(r => r.Delete(It.IsAny<Building>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteBuildingThatNotExist()
        {
            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object);

            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);

            _buildingRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns((Building)null);

            _buildingRepositoryMock.Setup(r => r.Delete(It.IsAny<Building>())).Verifiable();

            _buildingService.DeleteBuilding(3);

            _buildingRepositoryMock.Verify(r => r.Delete(It.IsAny<Building>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestDeleteBuildingNotManager()
        {
            var user = new Administrator();

            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object);

            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(user);

            _buildingService.DeleteBuilding(1);

            _buildingRepositoryMock.VerifyAll();
        }



        [TestMethod]
        public void ModifyBuilding()
        {
            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object);

            _buildingRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns(_building);

            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);

            _buildingRepositoryMock.Setup(r => r.Update(It.IsAny<Building>())).Verifiable();

            var modifiedBuilding = _building;
            modifiedBuilding.Expenses = 5000;
            modifiedBuilding.ConstructionCompany = new ConstructionCompany() { Id = 2, Name = "Constructora modificada" };

            var result = _buildingService.ModifyBuilding(1, modifiedBuilding);

            Assert.IsNotNull(result);
            Assert.AreEqual(modifiedBuilding, result);

            _buildingRepositoryMock.Verify(r => r.Update(It.IsAny<Building>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ModifyBuildingThatNotExist()
        {
            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object);

            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);

            _buildingRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns((Building)null);

            _buildingRepositoryMock.Setup(r => r.Update(It.IsAny<Building>())).Verifiable();

            var modifiedBuilding = new Building();

            _buildingService.ModifyBuilding(3, modifiedBuilding);

            _buildingRepositoryMock.Verify(r => r.Update(It.IsAny<Building>()), Times.Once);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestModifyBuildingNotManager()
        {
            var user = new Administrator();

            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object);

            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(user);

            var modifiedBuilding = new Building();

            _buildingService.ModifyBuilding(1, modifiedBuilding);
        }
    }
}