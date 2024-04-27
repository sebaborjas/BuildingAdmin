using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using IServices;
using Domain;
using Microsoft.IdentityModel.Tokens;
using WebApi.Controllers;
using DTO.Out;
using Microsoft.AspNetCore.Mvc;
using DTO.In;
using System.Net;

namespace TestWebApi
{
    [TestClass]
    public class TestBuildingController
    {

        private Mock<IBuildingServices> _buildingServices;

        [TestInitialize]
        public void Setup()
        {
            _buildingServices = new Mock<IBuildingServices>(MockBehavior.Strict);
        }

        [TestMethod]
        public void TestCreateBuilding()
        {
            var newBuilding = new Building()
            {
                Id = 11,
                Address = "Calle, 123, esquina",
                Expenses = 1000,
                ConstructionCompany = new ConstructionCompany(),
                Location = "111,111",
                Name = "Edificio nuevo"
            };
            
            _buildingServices.Setup(r => r.CreateBuilding(It.IsAny<Building>())).Returns(newBuilding);
            var buildingController = new BuildingController(_buildingServices.Object);
            var input = new CreateBuildingInput()
            {
                Name = "Edificio nuevo",
                Address = "Calle, 123, esquina",
                Location = "111,111",
                ConstructionCompany = "Empresa constructora",
                Expenses = 1000,
                Apartments = new List<NewApartmentInput>()
                {
                    new NewApartmentInput()
                    {
                        Floor = 1,
                        DoorNumber = 1,
                        OwnerName = "Nombre dueño",
                        OwnerLastName = "Apellido dueño",
                        OwnerEmail = "mail@mail.com",
                        Rooms = 2,
                        Bathrooms = 1,
                        HasTerrace = false
                    },
                    new NewApartmentInput()
                    {
                        Floor = 1,
                        DoorNumber = 2,
                        OwnerName = "Nombre dueño 2",
                        OwnerLastName = "Apellido dueño 2",
                        OwnerEmail = "mail2@mail.com",
                        Rooms = 3,
                        Bathrooms = 1,
                        HasTerrace = true
                    }
                }
            };

            var result = buildingController.CreateBuilding(input);
            var okResult = result as OkObjectResult;
            var newBuildingResponse = okResult.Value as CreateBuildingOutput;

            _buildingServices.VerifyAll();
            Assert.AreEqual(11, newBuildingResponse.BuildingId);
        }

        [TestMethod]
        public void TestCreateBuildingWithoutBody()
        {
            var buildingController = new BuildingController(_buildingServices.Object);
            CreateBuildingInput input = null;

            var result = buildingController.CreateBuilding(input);

            Assert.IsTrue(result.GetType().Equals(typeof(BadRequestResult)));
        }

        [TestMethod]
        public void TestCreateBuildingWithoutName()
        {
            var buildingController = new BuildingController(_buildingServices.Object);
            CreateBuildingInput input = new CreateBuildingInput()
            {
                Address = "Calle, 123, esquina",
                Location = "111,111",
                ConstructionCompany = "Empresa constructora",
                Expenses = 1000,
                Apartments = new List<NewApartmentInput>()
            };

            var result = buildingController.CreateBuilding(input);

            Assert.IsTrue(result.GetType().Equals(typeof(BadRequestResult)));
        }

        [TestMethod]
        public void TestCreateBuildingWithEmptyName()
        {
            var buildingController = new BuildingController(_buildingServices.Object);
            CreateBuildingInput input = new CreateBuildingInput()
            {
                Name = "",
                Address = "Calle, 123, esquina",
                Location = "111,111",
                ConstructionCompany = "Empresa constructora",
                Expenses = 1000,
                Apartments = new List<NewApartmentInput>()
            };

            var result = buildingController.CreateBuilding(input);

            Assert.IsTrue(result.GetType().Equals(typeof(BadRequestResult)));
        }

        [TestMethod]
        public void TestCreateBuildingWithoutAddress()
        {
            var buildingController = new BuildingController(_buildingServices.Object);
            CreateBuildingInput input = new CreateBuildingInput()
            {
                Name = "Edificio nuevo",
                Location = "111,111",
                ConstructionCompany = "Empresa constructora",
                Expenses = 1000,
                Apartments = new List<NewApartmentInput>()
            };

            var result = buildingController.CreateBuilding(input);

            Assert.IsTrue(result.GetType().Equals(typeof(BadRequestResult)));
        }

        [TestMethod]
        public void TestCreateBuildingWithEmptyAddress()
        {
            var buildingController = new BuildingController(_buildingServices.Object);
            CreateBuildingInput input = new CreateBuildingInput()
            {
                Name = "Edificio nuevo",
                Address = "",
                Location = "111,111",
                ConstructionCompany = "Empresa constructora",
                Expenses = 1000,
                Apartments = new List<NewApartmentInput>()
            };

            var result = buildingController.CreateBuilding(input);

            Assert.IsTrue(result.GetType().Equals(typeof(BadRequestResult)));
        }

        [TestMethod]
        public void TestCreateBuildingWithoutLocation()
        {
            var buildingController = new BuildingController(_buildingServices.Object);
            CreateBuildingInput input = new CreateBuildingInput()
            {
                Name = "Edificio nuevo",
                Address = "Calle, 123, esquina",
                ConstructionCompany = "Empresa constructora",
                Expenses = 1000,
                Apartments = new List<NewApartmentInput>()
            };

            var result = buildingController.CreateBuilding(input);

            Assert.IsTrue(result.GetType().Equals(typeof(BadRequestResult)));
        }

        [TestMethod]
        public void TestCreateBuildingWithEmptyLocation()
        {
            var buildingController = new BuildingController(_buildingServices.Object);
            CreateBuildingInput input = new CreateBuildingInput()
            {
                Name = "Edificio nuevo",
                Address = "Calle, 123, esquina",
                Location = "",
                ConstructionCompany = "Empresa constructora",
                Expenses = 1000,
                Apartments = new List<NewApartmentInput>()
            };

            var result = buildingController.CreateBuilding(input);

            Assert.IsTrue(result.GetType().Equals(typeof(BadRequestResult)));
        }

        [TestMethod]
        public void TestCreateBuildingWithoutConstructionCompany()
        {
            var buildingController = new BuildingController(_buildingServices.Object);
            CreateBuildingInput input = new CreateBuildingInput()
            {
                Name = "Edificio nuevo",
                Address = "Calle, 123, esquina",
                Location = "111,111",
                Expenses = 1000,
                Apartments = new List<NewApartmentInput>()
            };

            var result = buildingController.CreateBuilding(input);

            Assert.IsTrue(result.GetType().Equals(typeof(BadRequestResult)));
        }

        [TestMethod]
        public void TestCreateBuildingWithEmptyConstructionCompany()
        {
            var buildingController = new BuildingController(_buildingServices.Object);
            CreateBuildingInput input = new CreateBuildingInput()
            {
                Name = "Edificio nuevo",
                Address = "Calle, 123, esquina",
                Location = "111,111",
                ConstructionCompany = "",
                Expenses = 1000,
                Apartments = new List<NewApartmentInput>()
            };

            var result = buildingController.CreateBuilding(input);

            Assert.IsTrue(result.GetType().Equals(typeof(BadRequestResult)));
        }

        [TestMethod]
        public void TestCreateBuildingWithInvalidExpenses()
        {
            var buildingController = new BuildingController(_buildingServices.Object);
            CreateBuildingInput input = new CreateBuildingInput()
            {
                Name = "Edificio nuevo",
                Address = "Calle, 123, esquina",
                Location = "111,111",
                Expenses = -1000,
                ConstructionCompany = "Empresa constructora",
                Apartments = new List<NewApartmentInput>()
            };

            var result = buildingController.CreateBuilding(input);

            Assert.IsTrue(result.GetType().Equals(typeof(BadRequestResult)));
        }

        
        [TestMethod]
        public void TestCreateBuildingWithInvalidApartment()
        {
            var buildingController = new BuildingController(_buildingServices.Object);
            CreateBuildingInput input = new CreateBuildingInput()
            {
                Name = "Edificio nuevo",
                Address = "Calle, 123, esquina",
                Location = "111,111",
                Expenses = 1000,
                ConstructionCompany = "Empresa constructora",
                Apartments = new List<NewApartmentInput>()
                {
                    new NewApartmentInput()
                    {
                        Floor = 0,
                        DoorNumber = -1,
                        OwnerName = "",
                        OwnerLastName = "",
                        OwnerEmail = "",
                        Rooms = -1,
                        Bathrooms = -1,
                        HasTerrace = false
                    }
                }
            };

            var result = buildingController.CreateBuilding(input);

            Assert.IsTrue(result.GetType().Equals(typeof(BadRequestResult)));
        }

        [TestMethod]
        public void DeleteBuilding()
        {
            _buildingServices.Setup(r => r.DeleteBuilding(It.IsAny<int>()));
            var buildingController = new BuildingController(_buildingServices.Object);

            var result = buildingController.DeleteBuilding(10);

            _buildingServices.VerifyAll();
            Assert.IsTrue(result.GetType().Equals(typeof(OkResult)));
        }
    }
}
