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
        private Mock<ITicketService> _ticketService;
        private Apartment _apartment;

        [TestInitialize]
        public void Setup()
        {
            _ticketService = new Mock<ITicketService>();
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
            var newTicket = new Ticket()
            {
                Id = 1,
                Description = "Ventana rota",
                Apartment = _apartment,
                Category = new Category() { Id = 1, Name = "Maintenance" }
            };

            _ticketService.Setup(x => x.CreateTicket(It.IsAny<Ticket>())).Returns(newTicket);
            var ticketController = new TicketController(_ticketService.Object);
            
            var ticket = new TicketCreateModel()
            {
                Description = "Ventana rota",
                Apartment = _apartment,
                Category = new Category() { Id = 1, Name = "Maintenance" }
            };

            var result = ticketController.CreateTicket(ticket);
            var okResult = result as OkObjectResult;
            var newTicketResponse = okResult.Value as TicketModel;

            _ticketService.VerifyAll();
            Assert.AreEqual(1, newTicketResponse.Id);
        }

    }

}
