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
                Category = new Category() { Id = 1, Name = "Maintenance" }
            };

            _ticketServiceMock.Setup(x => x.CreateTicket(It.IsAny<Ticket>())).Returns(ticket);
            var ticketController = new TicketController(_ticketServiceMock.Object);

            var ticketCreateModel = new TicketCreateModel()
            {
                Description = "Ventana rota",
                Apartment = _apartment,
                Category = new Category() { Id = 1, Name = "Maintenance" }
            };

            var result = ticketController.CreateTicket(ticketCreateModel);
            var okResult = result as OkObjectResult;
            var newTicketResponse = okResult.Value as TicketModel;

            var expectedTicket = new TicketModel(ticket);

            _ticketServiceMock.VerifyAll();
            Assert.AreEqual(newTicketResponse, expectedTicket);
        }

        [TestMethod]
        public void TestCreateTicketBadRequest()
        {
            var ticketController = new TicketController(_ticketServiceMock.Object);
            var result = ticketController.CreateTicket(null);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
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
                    Category = new Category() { Id = 1, Name = "Maintenance" }
                },
                new Ticket()
                {
                    Description = "Limpieza grasera",
                    Apartment = _apartment,
                    Category = new Category() { Id = 2, Name = "Plumber" }
                }
            };

            _ticketServiceMock.Setup(x => x.GetTickets(It.IsAny<string>())).Returns(tickets);
            var ticketController = new TicketController(_ticketServiceMock.Object);

            var result = ticketController.GetTickets();
            var okResult = result as OkObjectResult;
            var ticketsResponse = okResult.Value as List<TicketModel>;

            var expectedTickets = new List<TicketModel>();
            foreach (var ticket in tickets)
            {
                expectedTickets.Add(new TicketModel(ticket));
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
            var ticketsResponse = okResult.Value as List<TicketModel>;

            var expectedTickets = new List<TicketModel>();

            _ticketServiceMock.VerifyAll();
            CollectionAssert.AreEqual(ticketsResponse, expectedTickets);
        }

    }

}
