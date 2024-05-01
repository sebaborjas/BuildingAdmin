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
