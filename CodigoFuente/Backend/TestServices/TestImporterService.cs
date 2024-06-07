using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Services;
using IServices;
using IImporter;
using Domain;
using DTO.In;
using DTO.Out;
namespace TestServices
{
    [TestClass]
    public class TestImporterService
    {
        private Mock<IBuildingService> _buildingServiceMock;
        private Mock<ISessionService> _sessionServiceMock;
        private ImportService _importService;

        [TestInitialize]
        public void Setup()
        {
            _buildingServiceMock = new Mock<IBuildingService>();
            _sessionServiceMock = new Mock<ISessionService>();
            _importService = new ImportService(_buildingServiceMock.Object, _sessionServiceMock.Object);
        }

        [TestMethod]
        public void GetAllImporters_ReturnsAvailableImporters()
        {

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ImportBuildings_ThrowsArgumentNullException_IfImporterNameOrPathIsNull()
        {
            string importerName = null;
            string path = "somePath";

            _importService.ImportBuildings(importerName, path);
            _importService.ImportBuildings("importerName", null);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ImportBuildings_ThrowsFileNotFoundException_IfFileDoesNotExist()
        {
            string importerName = "JSON";
            string path = "nonexistentFile.json";

            _importService.ImportBuildings(importerName, path);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ImportBuildings_ThrowsFileNotFoundException_IfImporterNotFound()
        {
            var importerMock = new Mock<ImporterInterface>();
            importerMock.Setup(i => i.GetName()).Returns("MockImporter");

            string importerName = "NonExistentImporter";
            string path = "somePath.json";

            _importService.ImportBuildings(importerName, path);
        }

        [TestMethod]
        public void ImportBuildings_CreatesBuildingsCorrectly()
        {
        }
    }

}

