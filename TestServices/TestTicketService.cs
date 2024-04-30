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
        private Mock<IGenericRepository<Building>> _buildingRepository;
        private Mock<IGenericRepository<Apartment>> _apartmentRepository;
        private Mock<IGenericRepository<Category>> _categoryRepository;
        private Mock<ISessionService> _sessionService;
        
        private Manager _user;
        private Building _building;
        private Apartment _apartment;
        private Category _category;

        [TestInitialize]
        public void Setup()
        {
            _ticketRepository = new Mock<IGenericRepository<Ticket>>(MockBehavior.Strict);
            _buildingRepository = new Mock<IGenericRepository<Building>>(MockBehavior.Strict);
            _apartmentRepository = new Mock<IGenericRepository<Apartment>>(MockBehavior.Strict);
            _categoryRepository = new Mock<IGenericRepository<Category>>(MockBehavior.Strict);
            _sessionService = new Mock<ISessionService>(MockBehavior.Strict);

            _category = new Category()
            {
                Id = 1,
                Name = "Sanitaria"
            };

            _user = new Manager()
            {
                Buildings = [],
                Email = "manager@correo.com",
                Id = 1,
                Name = "Lucas",
                LastName = "Gonzalez",
                Password = "Lu.Go123!",
            };

            _apartment = new Apartment()
            {
                Id = 1,
                Bathrooms = 2,
                DoorNumber = 3,
                Floor = 1,
                HasTerrace = false,
                Owner = new Owner(),
                Rooms = 2
            };

            _building = new Building()
            {
                Id = 1,
                Apartments = [_apartment],
                Address = "Rivera, 1111, Soca",
                ConstructionCompany = new ConstructionCompany(),
                Expenses = 2000,
                Location = "111,111",
                Name = "Edificio Las Delicias",
            };

        }

        [TestMethod]
        public void TestCreateTicket()
        {
            var ticket = new Ticket()
            {
                Category = new Category() { Id = 1 },
                Apartment = new Apartment() { Id = 1 },
                Description = "Perdida de agua",
            };
            _sessionService.Setup(r=>r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);
            _ticketRepository.Setup(r => r.Insert(It.IsAny<Ticket>())).Verifiable();
            _categoryRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(_category);
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object);

            var result = _ticketService.CreateTicket(ticket);

            _sessionService.VerifyAll();
            _ticketRepository.VerifyAll();
            Assert.AreEqual(result, ticket);
        }

        [TestMethod]
        public void TestTicketCreatedBy()
        {
            var ticket = new Ticket()
            {
                Category = new Category() { Id = 1 },
                Apartment = new Apartment() { Id = 1 },
                Description = "Perdida de agua",
            };
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);
            _ticketRepository.Setup(r => r.Insert(It.IsAny<Ticket>())).Verifiable();
            _categoryRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(_category);
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object);

            var result = _ticketService.CreateTicket(ticket);

            _sessionService.VerifyAll();
            _ticketRepository.VerifyAll();
            Assert.AreEqual(result.CreatedBy, _user);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void TestCreateTicketWithInvalidCategory()
        {
            var ticket = new Ticket()
            {
                Category = new Category() { Id = 2 },
                Apartment = new Apartment() { Id = 1 },
                Description = "Perdida de agua",
            };
            Category nullCategory = null;
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);
            _categoryRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(nullCategory);
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object);

            _ticketService.CreateTicket(ticket);
        }
    }
}
