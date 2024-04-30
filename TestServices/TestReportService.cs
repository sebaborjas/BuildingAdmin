using IDataAcess;
using Services;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using System.Linq.Expressions;

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

      _maintenanceOperatorRepositoryMock.VerifyAll();

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

    [TestMethod]
    public void TestGetTicketsByMaintenanceOperator()
    {

      MaintenanceOperator operatorUno = new MaintenanceOperator { Name = "Operator Uno" };
      MaintenanceOperator operatorDos = new MaintenanceOperator { Name = "Operator Dos" };

      Ticket ticketUno = new Ticket { Status = Domain.DataTypes.Status.Open, AssignedTo = operatorUno };
      ticketUno.AttendTicket();

      Ticket ticketDos = new Ticket { Status = Domain.DataTypes.Status.Open, AssignedTo = operatorUno };
      ticketDos.AttendTicket();
      ticketDos.CloseTicket(100);

      Ticket ticketTres = new Ticket { Status = Domain.DataTypes.Status.Open, AssignedTo = operatorUno };
      Ticket ticketCuatro = new Ticket { Status = Domain.DataTypes.Status.Open, AssignedTo = operatorDos };
      Ticket ticketCinco = new Ticket { Status = Domain.DataTypes.Status.Open, AssignedTo = operatorDos };
      ticketCinco.AttendTicket();
      Ticket ticketSeis = new Ticket { Status = Domain.DataTypes.Status.Open, AssignedTo = operatorDos };



      _buildingRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Building, bool>>>(), It.IsAny<List<string>>()))
      .Returns((Expression<Func<Building, bool>> predicate, List<string> includes) => new Building{
        Id = 1,
        Name = "Building Uno",
        Tickets = new List<Ticket>
        {
          ticketUno,
          ticketDos,
          ticketTres,
          ticketCuatro,
          ticketCinco,
          ticketSeis
        }
      });

      _reportService = new ReportsService(_ticketRepositoryMock.Object, _buildingRepositoryMock.Object, _maintenanceOperatorRepositoryMock.Object);

      var ticketsByOperator = _reportService.GetTicketsByMaintenanceOperator("Building Uno");

      var firstOperatorResult = ticketsByOperator.ElementAt(0);
      var secondOperatorResult = ticketsByOperator.ElementAt(1);

      _maintenanceOperatorRepositoryMock.VerifyAll();

      Assert.AreEqual(2, ticketsByOperator.Count);
      Assert.AreEqual("Operator Uno", firstOperatorResult.OperatorName);
      Assert.AreEqual(1, firstOperatorResult.TicketsOpen);
      Assert.AreEqual(1, firstOperatorResult.TicketsInProgress);
      Assert.AreEqual(1, firstOperatorResult.TicketsClosed);
      Assert.AreEqual("00:00", firstOperatorResult.AverageTimeToClose);

      Assert.AreEqual("Operator Dos", secondOperatorResult.OperatorName);
      Assert.AreEqual(2, secondOperatorResult.TicketsOpen);
      Assert.AreEqual(1, secondOperatorResult.TicketsInProgress);
      Assert.AreEqual(0, secondOperatorResult.TicketsClosed);
      Assert.AreEqual("00:00", secondOperatorResult.AverageTimeToClose);

    }

    [TestMethod]
    public void TestGetTicketsByCategory()
    {
      Category categoryUno = new Category { Name = "Category Uno" };
      Category categoryDos = new Category { Name = "Category Dos" };

      Ticket ticketUno = new Ticket { Status = Domain.DataTypes.Status.Open, Category = categoryUno };
      ticketUno.AttendTicket();

      Ticket ticketDos = new Ticket { Status = Domain.DataTypes.Status.Open, Category = categoryUno };
      ticketDos.AttendTicket();
      ticketDos.CloseTicket(100);

      Ticket ticketTres = new Ticket { Status = Domain.DataTypes.Status.Open, Category = categoryDos };

      Ticket ticketCuatro = new Ticket { Status = Domain.DataTypes.Status.Open, Category = categoryDos };

      Ticket ticketCinco = new Ticket { Status = Domain.DataTypes.Status.Open, Category = categoryDos };
      ticketCinco.AttendTicket();

      Ticket ticketSeis = new Ticket { Status = Domain.DataTypes.Status.Open, Category = categoryUno };



      _buildingRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Building, bool>>>(), It.IsAny<List<string>>()))
      .Returns((Expression<Func<Building, bool>> predicate, List<string> includes) => new Building{
        Id = 1,
        Name = "Building Uno",
        Tickets = new List<Ticket>
        {
          ticketUno,
          ticketDos,
          ticketTres,
          ticketCuatro,
          ticketCinco,
          ticketSeis
        }
      });

      _reportService = new ReportsService(_ticketRepositoryMock.Object, _buildingRepositoryMock.Object, _maintenanceOperatorRepositoryMock.Object);

      var ticketsByOperator = _reportService.GetTicketsByCategory("Building Uno");

      var firstOperatorResult = ticketsByOperator.ElementAt(0);
      var secondOperatorResult = ticketsByOperator.ElementAt(1);

      _maintenanceOperatorRepositoryMock.VerifyAll();

      Assert.AreEqual(2, ticketsByOperator.Count);
      Assert.AreEqual("Category Uno", firstOperatorResult.CategoryName);
      Assert.AreEqual(1, firstOperatorResult.TicketsOpen);
      Assert.AreEqual(1, firstOperatorResult.TicketsInProgress);
      Assert.AreEqual(1, firstOperatorResult.TicketsClosed);

      Assert.AreEqual("Category Dos", secondOperatorResult.CategoryName);
      Assert.AreEqual(2, secondOperatorResult.TicketsOpen);
      Assert.AreEqual(1, secondOperatorResult.TicketsInProgress);
      Assert.AreEqual(0, secondOperatorResult.TicketsClosed);


    }

  }
}