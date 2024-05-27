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

        private Mock<IBuildingService> _buildingServices;

        [TestInitialize]
        public void Setup()
        {
            _buildingServices = new Mock<IBuildingService>(MockBehavior.Strict);
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
        [ExpectedException(typeof(NullReferenceException))]
        public void TestCreateBuildingWithoutBody()
        {
            var buildingController = new BuildingController(_buildingServices.Object);
            CreateBuildingInput input = null;

            var result = buildingController.CreateBuilding(input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateBuildingWithoutName()
        {
            var buildingController = new BuildingController(_buildingServices.Object);
            CreateBuildingInput input = new CreateBuildingInput()
            {
                Address = "Calle, 123, esquina",
                Location = "111,111",
                Expenses = 1000,
                Apartments = new List<NewApartmentInput>()
            };

            var result = buildingController.CreateBuilding(input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateBuildingWithEmptyName()
        {
            var buildingController = new BuildingController(_buildingServices.Object);
            CreateBuildingInput input = new CreateBuildingInput()
            {
                Name = "",
                Address = "Calle, 123, esquina",
                Location = "111,111",
                Expenses = 1000,
                Apartments = new List<NewApartmentInput>()
            };

            var result = buildingController.CreateBuilding(input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateBuildingWithoutAddress()
        {
            var buildingController = new BuildingController(_buildingServices.Object);
            CreateBuildingInput input = new CreateBuildingInput()
            {
                Name = "Edificio nuevo",
                Location = "111,111",
                Expenses = 1000,
                Apartments = new List<NewApartmentInput>()
            };

            var result = buildingController.CreateBuilding(input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateBuildingWithEmptyAddress()
        {
            var buildingController = new BuildingController(_buildingServices.Object);
            CreateBuildingInput input = new CreateBuildingInput()
            {
                Name = "Edificio nuevo",
                Address = "",
                Location = "111,111",
                Expenses = 1000,
                Apartments = new List<NewApartmentInput>()
            };

            var result = buildingController.CreateBuilding(input);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateBuildingWithoutLocation()
        {
            var buildingController = new BuildingController(_buildingServices.Object);
            CreateBuildingInput input = new CreateBuildingInput()
            {
                Name = "Edificio nuevo",
                Address = "Calle, 123, esquina",
                Expenses = 1000,
                Apartments = new List<NewApartmentInput>()
            };

            var result = buildingController.CreateBuilding(input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateBuildingWithEmptyLocation()
        {
            var buildingController = new BuildingController(_buildingServices.Object);
            CreateBuildingInput input = new CreateBuildingInput()
            {
                Name = "Edificio nuevo",
                Address = "Calle, 123, esquina",
                Location = "",
                Expenses = 1000,
                Apartments = new List<NewApartmentInput>()
            };

            var result = buildingController.CreateBuilding(input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCreateBuildingWithInvalidExpenses()
        {
            var buildingController = new BuildingController(_buildingServices.Object);
            CreateBuildingInput input = new CreateBuildingInput()
            {
                Name = "Edificio nuevo",
                Address = "Calle, 123, esquina",
                Location = "111,111",
                Expenses = -1000,
                Apartments = new List<NewApartmentInput>()
            };

            var result = buildingController.CreateBuilding(input);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateBuildingWithInvalidApartment()
        {
            var buildingController = new BuildingController(_buildingServices.Object);
            CreateBuildingInput input = new CreateBuildingInput()
            {
                Name = "Edificio nuevo",
                Address = "Calle, 123, esquina",
                Location = "111,111",
                Expenses = 1000,
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

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void DeleteInvalidBuilding()
        {
            _buildingServices.Setup(r => r.DeleteBuilding(It.IsAny<int>())).Throws(new Exception());
            var buildingController = new BuildingController(_buildingServices.Object);

            var result = buildingController.DeleteBuilding(10);

            _buildingServices.VerifyAll();
        }

        [TestMethod]
        public void TestModifyBuilding()
        {
            var buildingModified = new Building()
            {
                Id = 10,
                Expenses = 1000,
                ConstructionCompany = new ConstructionCompany(),
            };
            _buildingServices.Setup(r => r.ModifyBuilding(It.IsAny<int>(), It.IsAny<Building>())).Returns(buildingModified);
            var buildingController = new BuildingController(_buildingServices.Object);

            var input = new ModifyBuildingInput()
            {
                ConstructionCompany = "Empresa nueva",
                Expenses = 4000,
                Apartments = new List<ModifyApartmentInput>() {
                    new ModifyApartmentInput()
                    {
                        Id = 1,
                        OwnerName = "Nuevo dueño",
                        OwnerLastName = "Apellido nuevo dueño",
                        OwnerEmail = "nuevoCorreo@mail.com"
                    }
                }
            };
            var result = buildingController.ModifyBuilding(10, input);

            _buildingServices.VerifyAll();
            Assert.IsTrue(result.GetType().Equals(typeof(OkResult)));
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void TestModifyInvalidBuilding()
        {
            _buildingServices.Setup(r => r.ModifyBuilding(It.IsAny<int>(), It.IsAny<Building>())).Throws(new KeyNotFoundException());
            var buildingController = new BuildingController(_buildingServices.Object);

            var input = new ModifyBuildingInput()
            {
                ConstructionCompany = "Empresa nueva",
                Expenses = 4000
            };
            var result = buildingController.ModifyBuilding(100, input);

            _buildingServices.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestModifyBuildingWithInvalidData()
        {
            var buildingController = new BuildingController(_buildingServices.Object);

            var input = new ModifyBuildingInput()
            {
                ConstructionCompany = "",
                Expenses = -1000
            };
            var result = buildingController.ModifyBuilding(100, input);

            _buildingServices.VerifyAll();
        }

        [TestMethod]
        public void TestGetAll()
        {
            var buildings = new List<Building>()
            {
                new Building()
                {
                    Id = 11,
                    Address = "Calle, 123, esquina",
                    Expenses = 1000,
                    ConstructionCompany = new ConstructionCompany(),
                    Location = "111,111",
                    Name = "Edificio nuevo"
                },
                new Building()
                {
                    Id = 12,
                    Address = "Otracalle, 123, otraesquina",
                    Expenses = 1000,
                    ConstructionCompany = new ConstructionCompany(),
                    Location = "222,222",
                    Name = "Otro edificio"
                }
            };
            _buildingServices.Setup(r => r.GetAllBuildingsForUser()).Returns(buildings);
            var buildingController = new BuildingController(_buildingServices.Object);
            var buildingsExpected = new List<GetBuildingOutput>();
            buildings.ForEach(building =>
            {
                buildingsExpected.Add(new GetBuildingOutput(building));
            });

            var result = buildingController.Get(null);
            var okResult = result as OkObjectResult;
            var buildingsRecieved = okResult.Value as List<GetBuildingOutput>;

            _buildingServices.VerifyAll();
            CollectionAssert.AreEqual(buildingsRecieved, buildingsExpected);
        }

        [TestMethod]
        public void TestGetBuilding()
        {
            var building = new Building()
            {
                Id = 11,
                Address = "Calle, 123, esquina",
                Expenses = 1000,
                ConstructionCompany = new ConstructionCompany()
                {
                    Id = 1,
                    Name = "Empresa constructora"
                },
                Location = "111,111",
                Name = "Edificio nuevo",
                Apartments = new List<Apartment>()
                {
                    new Apartment()
                    {
                        Id = 1,
                        Floor = 1,
                        DoorNumber = 1,
                        Owner = new Owner()
                        {
                            Id = 1,
                            Name = "Nombre",
                            LastName = "Apellido",
                            Email = "test@email.com"
                        },
                        Rooms = 2,
                        Bathrooms = 1,
                        HasTerrace = false
                    }
                }
            };
            _buildingServices.Setup(r => r.Get(It.IsAny<int>())).Returns(building);
            var buildingController = new BuildingController(_buildingServices.Object);
            var expectedResult = new GetBuildingOutput(building);

            var result = buildingController.Get(11);
            var okResult = result as OkObjectResult;
            var buildingRecieved = okResult.Value as GetBuildingOutput;

            _buildingServices.VerifyAll();
            Assert.AreEqual(buildingRecieved, expectedResult);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestGetBuildingNotFound()
        {
            Building building = null;
            _buildingServices.Setup(r => r.Get(It.IsAny<int>())).Returns(building);
            var buildingController = new BuildingController(_buildingServices.Object);

            var result = buildingController.Get(11);

            _buildingServices.VerifyAll();
        }
    }
}
