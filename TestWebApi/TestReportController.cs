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

        private Mock<IReportServices> _reportServices;

        [TestMethod]
        public void TestGetRequestsByBuildingReportForAll()
        {

        }
    }
}
