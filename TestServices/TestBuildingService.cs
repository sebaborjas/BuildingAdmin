using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services;
using IDataAccess;
using Domain;
using IDataAcess;
using System.Linq.Expressions;

namespace TestServices
{
  [TestClass]
  public class TestBuildingService
  {
    BuildingService _buildingService;

    Mock<IGenericRepository<Building>> _buildingRepositoryMock;

    [TestInitialize]
    public void SetUp()
    {
      _buildingRepositoryMock = new Mock<IGenericRepository<Building>>(MockBehavior.Strict);
    }

    [TestMethod]
    public void CreateBuilding()
    {
      _buildingRepositoryMock.Setup(r => r.Insert(It.IsAny<Building>())).Verifiable();

      _buildingRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Building, bool>>>(), It.IsAny<List<string>>()))
        .Returns((Expression<Func<Building, bool>> predicate, List<string> includes) => null);

      _buildingService = new BuildingService(_buildingRepositoryMock.Object);

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

      _buildingService = new BuildingService(_buildingRepositoryMock.Object);

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
  }
}