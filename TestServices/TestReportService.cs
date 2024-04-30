using IDataAcess;
using Services;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;

namespace ProjectNamespace.Test
{
  [TestClass]
  public class TestReportService
  {

    ReportsService _reportService;
    Mock<IGenericRepository<Ticket>> _ticketRepositoryMock;
    Mock<IGenericRepository<Building>> _buildingRepositoryMock;
    Mock<IGenericRepository<MaintenanceOperator>> _maintenanceOperatorRepositoryMock;

    [TestInitialize]
    public void SetUp()
    {
      _ticketRepositoryMock = new Mock<IGenericRepository<Ticket>>();
      _buildingRepositoryMock = new Mock<IGenericRepository<Building>>();
      _maintenanceOperatorRepositoryMock = new Mock<IGenericRepository<MaintenanceOperator>>();
    }


    [TestMethod]
    public void TestGetTicketsByBuilding()
    {
      _buildingRepositoryMock.Setup(r => r.GetAll<Building>()).Returns(new List<Building>
      {
        new Building
        {
          Id = 1,
          Name = "Building Uno",
          Tickets = new List<Ticket>
          {
            new Ticket { Status = Domain.DataTypes.Status.Open },
            new Ticket { Status = Domain.DataTypes.Status.InProgress },
            new Ticket { Status = Domain.DataTypes.Status.InProgress }
          }
        },
        new Building
        {
          Id = 2,
          Name = "Building Dos",
          Tickets = new List<Ticket>
          {
            new Ticket { Status = Domain.DataTypes.Status.Closed },
            new Ticket { Status = Domain.DataTypes.Status.InProgress },
            new Ticket { Status = Domain.DataTypes.Status.Closed }
          }
        }
      });

      _reportService = new ReportsService(_ticketRepositoryMock.Object, _buildingRepositoryMock.Object, _maintenanceOperatorRepositoryMock.Object);

      var ticketsByBuilding = _reportService.GetTicketsByBuilding();

      Assert.AreEqual(2, ticketsByBuilding.Count);
      Assert.AreEqual("Building Uno", ticketsByBuilding.ElementAt(0).BuildingName);
      Assert.AreEqual(1, ticketsByBuilding.ElementAt(0).TicketsOpen);
      Assert.AreEqual(2, ticketsByBuilding.ElementAt(0).TicketsInProgress);
      Assert.AreEqual(0, ticketsByBuilding.ElementAt(0).TicketsClosed);
      Assert.AreEqual("Building Dos", ticketsByBuilding.ElementAt(1).BuildingName);
      Assert.AreEqual(0, ticketsByBuilding.ElementAt(1).TicketsOpen);
      Assert.AreEqual(1, ticketsByBuilding.ElementAt(1).TicketsInProgress);
      Assert.AreEqual(2, ticketsByBuilding.ElementAt(1).TicketsClosed);


    }
  }
}