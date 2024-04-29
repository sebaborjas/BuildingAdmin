using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services;
using IDataAccess;
using Domain;
using IDataAcess;

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
  }
}