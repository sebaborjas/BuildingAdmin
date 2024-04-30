using Domain;
using IDataAcess;
using IServices;
using Moq;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServices
{
    [TestClass]
    public class TestTicketService
    {
        private ITicketService _ticketService;
        private Mock<IGenericRepository<Ticket>> _ticketRepository;
        private Mock<ISessionService> _sessionService;

        [TestInitialize]
        public void Setup()
        {
            _ticketRepository = new Mock<IGenericRepository<Ticket>>(MockBehavior.Strict);
            _sessionService = new Mock<ISessionService>(MockBehavior.Strict);
        }

        [TestMethod]
        public void TestCreateTicket()
        {
            var user = new Manager()
            {
                Buildings = [],
                Email = "manager@correo.com",
                Id = 1,
                Name = "Lucas",
                LastName = "Gonzalez",
                Password = "Lu.Go123!",
            };
            var ticket = new Ticket()
            {
                Category = new Category(),
                Apartment = new Apartment(),
                Description = "Perdida de agua",
            };
            _sessionService.Setup(r=>r.GetCurrentUser(It.IsAny<Guid?>())).Returns(user);
            _ticketRepository.Setup(r => r.Insert(It.IsAny<Ticket>())).Verifiable();
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object);

            var result = _ticketService.CreateTicket(ticket);

            _sessionService.VerifyAll();
            _ticketRepository.VerifyAll();
            Assert.AreEqual(result, ticket);
        }

        [TestMethod]
        public void TestTicketCreatedBy()
        {
            var user = new Manager()
            {
                Buildings = [],
                Email = "manager@correo.com",
                Id = 1,
                Name = "Lucas",
                LastName = "Gonzalez",
                Password = "Lu.Go123!",
            };
            var ticket = new Ticket()
            {
                Category = new Category(),
                Apartment = new Apartment(),
                Description = "Perdida de agua",
            };
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(user);
            _ticketRepository.Setup(r => r.Insert(It.IsAny<Ticket>())).Verifiable();
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object);

            var result = _ticketService.CreateTicket(ticket);

            _sessionService.VerifyAll();
            _ticketRepository.VerifyAll();
            Assert.AreEqual(result.CreatedBy, user);
        }
    }
}
