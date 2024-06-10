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
    public class TestTicketController
    {
        private Mock<ITicketService> _ticketServiceMock;
        private Apartment _apartment;

        [TestInitialize]
        public void Setup()
        {
            _ticketServiceMock = new Mock<ITicketService>();
            _apartment = new Apartment()
            {
                Id = 1,
                Owner = new Owner()
                {
                    Id = 1,
                    Name = "Juan",
                    LastName = "Perez",
                    Email = "juan@test.com"
                },
                Bathrooms = 1,
                HasTerrace = false,
                DoorNumber = 304,
                Floor = 1,
                Rooms = 1
            };
        }

        [TestMethod]
        public void TestCreateTicket()
        {
            Ticket ticket = new Ticket()
            {
                Description = "Ventana rota",
                Apartment = _apartment,
                Category = new Category() { Id = 1, Name = "Maintenance" },
                CreatedBy = new Manager()
                {
                    Id = 1,
                    Name = "Juan",
                    LastName = "Perez",
                    Email = "mail@mail.com"
                }
                
            };

            _ticketServiceMock.Setup(x => x.CreateTicket(It.IsAny<Ticket>())).Returns(ticket);
            var ticketController = new TicketController(_ticketServiceMock.Object);

            var ticketCreateModel = new TicketCreateInput()
            {
                Description = "Ventana rota",
                ApartmentId = 1,
                CategoryId = 1
            };

            var result = ticketController.CreateTicket(ticketCreateModel);
            var okResult = result as OkObjectResult;
            var ticketResponse = okResult.Value as TicketOutput;

            var expectedTicket = new TicketOutput(ticket);
            _ticketServiceMock.VerifyAll();
            Assert.AreEqual(ticketResponse, expectedTicket);
        }
        
        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void TestCreateTicketBadRequest()
        {
            _ticketServiceMock.Setup(x => x.CreateTicket(It.IsAny<Ticket>())).Throws(new InvalidDataException());
            var ticketController = new TicketController(_ticketServiceMock.Object);

            var ticketCreateModel = new TicketCreateInput()
            {
                Description = "Ventana rota",
                ApartmentId = 1,
                CategoryId = 1
            };

            var result = ticketController.CreateTicket(ticketCreateModel);
            _ticketServiceMock.VerifyAll();
        }

        [TestMethod]
        public void TestGetTickets()
        {
            List<Ticket> tickets = new List<Ticket>()
            {
                new Ticket()
                {
                    Description = "Ventana rota",
                    Apartment = _apartment,
                    Category = new Category() { Id = 1, Name = "Maintenance" },
                    CreatedBy = new Manager()
                    {
                        Id = 1,
                        Name = "Manager"
                    }
                },
                new Ticket()
                {
                    Description = "Limpieza grasera",
                    Apartment = _apartment,
                    Category = new Category() { Id = 2, Name = "Plumber" },
                    CreatedBy = new Manager()
                    {
                        Id = 1,
                        Name = "Manager"
                    }
                }
            };

            _ticketServiceMock.Setup(x => x.GetTickets(It.IsAny<string>())).Returns(tickets);
            var ticketController = new TicketController(_ticketServiceMock.Object);

            var result = ticketController.GetTickets();
            var okResult = result as OkObjectResult;
            var ticketsResponse = okResult.Value as List<TicketOutput>;

            var expectedTickets = new List<TicketOutput>();
            foreach (var ticket in tickets)
            {
                expectedTickets.Add(new TicketOutput(ticket));
            }

            _ticketServiceMock.VerifyAll();
            CollectionAssert.AreEqual(ticketsResponse, expectedTickets);
        }

        [TestMethod]
        public void TestGetTicketBadRequest()
        {
            _ticketServiceMock.Setup(x => x.GetTickets(It.IsAny<string>())).Returns(new List<Ticket>());
            var ticketController = new TicketController(_ticketServiceMock.Object);

            var result = ticketController.GetTickets();
            var okResult = result as OkObjectResult;
            var ticketsResponse = okResult.Value as List<TicketOutput>;

            var expectedTickets = new List<TicketOutput>();

            _ticketServiceMock.VerifyAll();
            CollectionAssert.AreEqual(ticketsResponse, expectedTickets);
        }

        [TestMethod]
        public void TestAssignTicket()
        {
            Ticket ticket = new Ticket()
            {
                Description = "Ventana rota",
                Apartment = _apartment,
                Category = new Category() { Id = 1, Name = "Maintenance" },
                CreatedBy = new Manager()
                {
                    Id = 1,
                    Name = "Juan",
                    LastName = "Perez",
                    Email = "mail@mail.com"
                }
            };

            _ticketServiceMock.Setup(x => x.AssignTicket(It.IsAny<int>(), It.IsAny<int>())).Returns(ticket);
            var ticketController = new TicketController(_ticketServiceMock.Object);

            var assignTicketInput = new AssignTicketInput()
            {
               MaintenanceOperatorId = 1
            };  

            var result = ticketController.AssignTicket(1, assignTicketInput);
            var okResult = result as OkObjectResult;
            var ticketResponse = okResult.Value as TicketOutput;

            var expectedTicket = new TicketOutput(ticket);
            _ticketServiceMock.VerifyAll();
            Assert.AreEqual(ticketResponse, expectedTicket);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void TestAssignTicketNotFound()
        {
            _ticketServiceMock.Setup(x => x.AssignTicket(It.IsAny<int>(), It.IsAny<int>())).Throws(new InvalidDataException());
            var ticketController = new TicketController(_ticketServiceMock.Object);

            var assignTicketInput = new AssignTicketInput()
            {
               MaintenanceOperatorId = 1
            };

            var result = ticketController.AssignTicket(1, assignTicketInput);
            _ticketServiceMock.VerifyAll();
        }

        [TestMethod]
        public void TestStartTicket()
        {
            Ticket ticket = new Ticket()
            {
                Description = "Ventana rota",
                Apartment = _apartment,
                Category = new Category() { Id = 1, Name = "Maintenance" },
                CreatedBy = new Manager()
                {
                    Id = 1,
                    Name = "Manager"
                }
            };

            _ticketServiceMock.Setup(x => x.StartTicket(It.IsAny<int>())).Returns(ticket);
            var ticketController = new TicketController(_ticketServiceMock.Object);

            var result = ticketController.StartTicket(1);
            var okResult = result as OkObjectResult;
            var ticketResponse = okResult.Value as TicketOutput;

            var expectedTicket = new TicketOutput(ticket);
            _ticketServiceMock.VerifyAll();
            Assert.AreEqual(ticketResponse, expectedTicket);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void TestStartTicketNotFound()
        {
            _ticketServiceMock.Setup(x => x.StartTicket(It.IsAny<int>())).Throws(new InvalidDataException());
            var ticketController = new TicketController(_ticketServiceMock.Object);

            var result = ticketController.StartTicket(1);
            _ticketServiceMock.VerifyAll();
        }

        [TestMethod]
        public void TestCompleteTicket()
        {
            Ticket ticket = new Ticket()
            {
                Description = "Ventana rota",
                Apartment = _apartment,
                Category = new Category() { Id = 1, Name = "Maintenance" },
                CreatedBy = new Manager()
                {
                    Id = 1,
                    Name = "Manager"
                }
            };

            _ticketServiceMock.Setup(x => x.CompleteTicket(It.IsAny<int>(), It.IsAny<float>())).Returns(ticket);
            var ticketController = new TicketController(_ticketServiceMock.Object);

            var completeTicketInput = new TicketCompleteInput()
            {
                TotalCost = 100
            };

            var result = ticketController.CompleteTicket(1, completeTicketInput);
            var okResult = result as OkObjectResult;
            var ticketResponse = okResult.Value as TicketOutput;

            var expectedTicket = new TicketOutput(ticket);
            _ticketServiceMock.VerifyAll();
            Assert.AreEqual(ticketResponse, expectedTicket);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void TestCompleteTicketNotFound()
        {
            _ticketServiceMock.Setup(x => x.CompleteTicket(It.IsAny<int>(), It.IsAny<float>())).Throws(new InvalidDataException());
            var ticketController = new TicketController(_ticketServiceMock.Object);

            var completeTicketInput = new TicketCompleteInput()
            {
                TotalCost = 100
            };

            var result = ticketController.CompleteTicket(1, completeTicketInput);
            _ticketServiceMock.VerifyAll();
        }

        [TestMethod]
        public void TestGetAssignedTickets()
        {
            List<Ticket> tickets = new List<Ticket>()
            {
                new Ticket()
                {
                    Description = "Ventana rota",
                    Apartment = _apartment,
                    Category = new Category() { Id = 1, Name = "Maintenance" },
                    CreatedBy = new Manager()
                    {
                        Id = 1,
                        Name = "Manager"
                    }
                },
                new Ticket()
                {
                    Description = "Limpieza grasera",
                    Apartment = _apartment,
                    Category = new Category() { Id = 2, Name = "Plumber" },
                    CreatedBy = new Manager()
                    {
                        Id = 1,
                        Name = "Manager"
                    }
                }
            };

            _ticketServiceMock.Setup(x => x.GetAssignedTickets()).Returns(tickets);
            var ticketController = new TicketController(_ticketServiceMock.Object);

            var result = ticketController.GetAssignedTickets();
            var okResult = result as OkObjectResult;
            var ticketsResponse = okResult.Value as List<TicketOutput>;

            var expectedTickets = new List<TicketOutput>();
            foreach (var ticket in tickets)
            {
                expectedTickets.Add(new TicketOutput(ticket));
            }

            _ticketServiceMock.VerifyAll();
            CollectionAssert.AreEqual(ticketsResponse, expectedTickets);
        }
        
    }


}
