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
            var expectedResult = new List<TicketsByMaintenanceOperator>
            {
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

            var result = _reportController.GetTicketsByMaintenanceOperator("Building1", "Operator2") as OkObjectResult;

            _reportServicesMock.VerifyAll();

            Assert.AreEqual(expectedResult, result.Value);
        }

        [TestMethod]
        public void TestGetTicketsCategory()
        {
            var expectedResult = new List<TicketsByCategory>
            {
                new TicketsByCategory
                {
                    CategoryName = "Category1",
                    TicketsOpen = 1,
                    TicketsInProgress = 2,
                    TicketsClosed = 3
                },
                new TicketsByCategory
                {
                    CategoryName = "Category2",
                    TicketsOpen = 4,
                    TicketsInProgress = 5,
                    TicketsClosed = 6
                }
            };

            _reportServicesMock.Setup(s => s.GetTicketsByCategory(It.IsAny<string>(), It.IsAny<string?>())).Returns(expectedResult);

            _reportController = new ReportController(_reportServicesMock.Object);

            var result = _reportController.GetTicketsByCategory("Building1") as OkObjectResult;

            _reportServicesMock.VerifyAll();

            Assert.AreEqual(expectedResult, result.Value);
        }

        [TestMethod]
        public void TestGetTicketsByApartmentReportForBuilding()
        {
            var expectedResult = new List<TicketByApartment>
            {
                new TicketByApartment
                {
                    ApartmentAndOwner = "1B - Jose Rodriguez",
                    TicketsOpen = 4,
                    TicketsClosed = 2,
                    TicketsInProgress = 0,
                },
                new TicketByApartment
                {
                    ApartmentAndOwner = "3B - Miguel Angel",
                    TicketsOpen = 0,
                    TicketsClosed = 0,
                    TicketsInProgress = 0,
                }
            };

            _reportServicesMock.Setup(s => s.GetTicketsByApartment(It.IsAny<string>())).Returns(expectedResult);

            _reportController = new ReportController(_reportServicesMock.Object);

            var result = _reportController.GetTicketsByApartment("EdificioUno") as OkObjectResult;

            _reportServicesMock.VerifyAll();

            Assert.AreEqual(expectedResult, result.Value);
        }

        [TestMethod]
        public void TestGetTicketsByBuildingReportForBuilding()
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
            };

            _reportServicesMock.Setup(s => s.GetTicketsByBuilding(It.IsAny<string?>())).Returns(expectedResult);

            _reportController = new ReportController(_reportServicesMock.Object);

            var result = _reportController.GetTicketsByBuilding("Building1") as OkObjectResult;

            _reportServicesMock.VerifyAll();

            Assert.AreEqual(expectedResult, result.Value);
        }

        [TestMethod]
        public void TestGetTicketsCategoryForCategory()
        {
            var expectedResult = new List<TicketsByCategory>
            {
                new TicketsByCategory
                {
                    CategoryName = "Category1",
                    TicketsOpen = 1,
                    TicketsInProgress = 2,
                    TicketsClosed = 3
                },
            };

            _reportServicesMock.Setup(s => s.GetTicketsByCategory(It.IsAny<string>(), It.IsAny<string?>())).Returns(expectedResult);

            _reportController = new ReportController(_reportServicesMock.Object);

            var result = _reportController.GetTicketsByCategory("Building1", "Category1") as OkObjectResult;

            _reportServicesMock.VerifyAll();

            Assert.AreEqual(expectedResult, result.Value);
        }
    }
}
