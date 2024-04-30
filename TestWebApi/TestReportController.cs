using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using DTO.Out;
using IServices;
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
        public void TestGetRequestsByBuildingReportForAll()
        {
            _reportServicesMock.Setup(x => x.GetRequestsByBuilding<string, Object>(It.IsAny<int?>()))
                .Returns(new Dictionary<string, Object>());

            _reportController = new ReportController(_reportServicesMock.Object);

            var result = _reportController.GetRequestsByBuilding();

            var okResult = result as OkObjectResult;

            var expectedResult = new Dictionary<string, Object>();

            _reportServicesMock.VerifyAll();

            CollectionAssert.AreEqual(expectedResult, okResult.Value as Dictionary<string, Object>);
        }

        [TestMethod]
        public void TestGetRequestsByBuildingReportForSpecificId()
        {
            _reportServicesMock.Setup(x => x.GetRequestsByBuilding<string, Object>(It.IsAny<int?>()))
                .Returns(new Dictionary<string, Object>());

            _reportController = new ReportController(_reportServicesMock.Object);

            var result = _reportController.GetRequestsByBuilding(1);

            var okResult = result as OkObjectResult;

            var expectedResult = new Dictionary<string, Object>();

            _reportServicesMock.VerifyAll();

            CollectionAssert.AreEqual(expectedResult, okResult.Value as Dictionary<string, Object>);
        }
    }
}
