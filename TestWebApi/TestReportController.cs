using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using DTO.Out;
using IServices;
using Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;

namespace TestWebApi
{
    [TestClass]
    public class TestReportController
    {
        private ReportController _reportController;
        private Mock<IReportServices> _reportServicesMock;

        [TestInitialize]
        public void SetUp()
        {
            _reportServicesMock = new Mock<IReportServices>(MockBehavior.Strict);
        }

        [TestMethod]
        public void TestGetTicketsByBuildingReportForAll()
        {
            var expectedResult = new List<TicketByBuilding>
            {
                new TicketByBuilding
                {
                    BuildingName = "Building1",
                    TicketsOpen = 1,
                    TicketsInProgress = 2,
                    TicketsClosed = 3
                },
                new TicketByBuilding
                {
                    BuildingName = "Building2",
                    TicketsOpen = 4,
                    TicketsInProgress = 5,
                    TicketsClosed = 6
                }
            };

            _reportServicesMock.Setup(s => s.GetTicketsByBuilding(It.IsAny<string?>())).Returns(expectedResult);

            _reportController = new ReportController(_reportServicesMock.Object);

            var result = _reportController.GetTicketsByBuilding() as OkObjectResult;

            _reportServicesMock.VerifyAll();

            Assert.AreEqual(expectedResult, result.Value);
        }

        [TestMethod]
        public void TestGetTicketsForSpecificBuildingReport()
        {
            var expectedResult = new List<TicketByBuilding>
            {
                new TicketByBuilding
                {
                    BuildingName = "Building1",
                    TicketsOpen = 1,
                    TicketsInProgress = 2,
                    TicketsClosed = 3
                }
            };

            _reportServicesMock.Setup(s => s.GetTicketsByBuilding(It.IsAny<string>())).Returns(expectedResult);

            _reportController = new ReportController(_reportServicesMock.Object);

            var result = _reportController.GetTicketsByBuilding() as OkObjectResult;

            _reportServicesMock.VerifyAll();

            Assert.AreEqual(expectedResult, result.Value);
        }
            

        [TestMethod]
        public void TestGetTicketsByMaintenanceOperator()
        {
            var expectedResult = new List<TicketsByMaintenanceOperator>
            {
                new TicketsByMaintenanceOperator
                {
                    OperatorName = "Operator1",
                    TicketsOpen = 1,
                    TicketsInProgress = 2,
                    TicketsClosed = 3,
                    AverageTimeToClose = "01:00"
                },
                new TicketsByMaintenanceOperator
                {
                    OperatorName = "Operator2",
                    TicketsOpen = 4,
                    TicketsInProgress = 5,
                    TicketsClosed = 6,
                    AverageTimeToClose = "02:00"
                }
            };

            _reportServicesMock.Setup(s => s.GetTicketsByMaintenanceOperator(It.IsAny<string>(), It.IsAny<string?>())).Returns(expectedResult);

            _reportController = new ReportController(_reportServicesMock.Object);

            var result = _reportController.GetTicketsByMaintenanceOperator("Building1") as OkObjectResult;

            _reportServicesMock.VerifyAll();

            Assert.AreEqual(expectedResult, result.Value);
        }

        [TestMethod]
        public void TestGetTicketsBySpecificMaintenanceOperator()
        {
        }

        [TestMethod]
        public void TestGetTicketsCategory()
        {
            
        }
    }
}
