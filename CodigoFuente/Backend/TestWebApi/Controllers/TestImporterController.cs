using Domain;
using IServices;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using DTO.In;
using DTO.Out;
using Microsoft.AspNetCore.Mvc;
using IImporter;
using System.IO;

namespace TestWebApi
{
    [TestClass]
    public class TestImporterController
    {
        private Mock<IImportService> _importServiceMock;
        private Mock<IBuildingService> _buildingServiceMock;
        private ImporterController _controller;

        [TestInitialize]
        public void Setup()
        {
            _importServiceMock = new Mock<IImportService>();
            _buildingServiceMock = new Mock<IBuildingService>();
            _controller = new ImporterController(_importServiceMock.Object, _buildingServiceMock.Object);
        }

        [TestMethod]
        public void TestGetAvailableImporters()
        {
            var importer = new Mock<ImporterInterface>();
            importer.Setup(x => x.GetName()).Returns("JSON");
            var importers = new List<ImporterInterface> { importer.Object };

            _importServiceMock.Setup(x => x.GetAllImporters()).Returns(importers);

            var result = _controller.GetAvailableImporters();

            var okResult = result as OkObjectResult;
            var importersList = okResult.Value as List<string>;

            CollectionAssert.AreEqual(new List<string> { "JSON" }, importersList);
            _importServiceMock.VerifyAll();
        }

        [TestMethod]
        public void TestGetAvailableImportersWithNoImporters()
        {
            var importers = new List<ImporterInterface>();
            _importServiceMock.Setup(x => x.GetAllImporters()).Returns(importers);
            var result = _controller.GetAvailableImporters();


            var okResult = result as OkObjectResult;
            var importersList = okResult.Value as List<string>;

            Assert.AreEqual(0, importersList.Count);

            _importServiceMock.VerifyAll();
        }

        [TestMethod]
        public void TestImportBuildings()
        {

            var buildings = new List<Building>
            {
                new Building
                {
                    Name = "Edificio 1",
                    Location = "-34.603722, -58.381592",
                    Address = "Calle, 1234, Esquina",
                    Expenses = 1000,
                    Apartments = new List<Apartment>() 
                }
            };

            var importerOutput = new ImporterOutput { CreatedBuildings = buildings.Select(b => new CreateBuildingOutput(b)).ToList(), Errors = null };

            _importServiceMock.Setup(x => x.ImportBuildings("JSON", "path")).Returns(importerOutput);

            var result = _controller.ImportBuildings(new ImporterInput { ImporterName = "JSON", Path = "path" });

            _importServiceMock.Verify(x => x.ImportBuildings("JSON", "path"), Times.Once);


            var okResult = result as OkObjectResult;
            var output = okResult.Value as ImporterOutput;

            Assert.AreEqual(importerOutput, output);
        }

        [TestMethod]
        public void TestImportBuildingsWithNoBuildings()
        {
            var importerOutput = new ImporterOutput { CreatedBuildings = new List<CreateBuildingOutput>(), Errors = null };

            _importServiceMock.Setup(x => x.ImportBuildings("JSON", "path")).Returns(importerOutput);

            var result = _controller.ImportBuildings(new ImporterInput { ImporterName = "JSON", Path = "path" });

            _importServiceMock.Verify(x => x.ImportBuildings("JSON", "path"), Times.Once);

            var okResult = result as OkObjectResult;
            var output = okResult.Value as ImporterOutput;

            Assert.AreEqual(importerOutput, output);
        }
    }
}
