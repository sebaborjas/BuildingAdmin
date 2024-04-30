using Domain;
using IDataAcess;
using IServices;
using Moq;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestServices
{
    [TestClass]
    public class TestTicketService
    {
        private ITicketService _ticketService;
        private Mock<IGenericRepository<Ticket>> _ticketRepository;
        private Mock<IGenericRepository<Category>> _categoryRepository;
        private Mock<IGenericRepository<MaintenanceOperator>> _maintenanceOperatorRepository;
        private Mock<ISessionService> _sessionService;
        
        private Manager _user;
        private Building _building;
        private Apartment _apartment;
        private Category _category;
        private MaintenanceOperator _maintenance;

        [TestInitialize]
        public void Setup()
        {
            _ticketRepository = new Mock<IGenericRepository<Ticket>>(MockBehavior.Strict);
            _categoryRepository = new Mock<IGenericRepository<Category>>(MockBehavior.Strict);
            _sessionService = new Mock<ISessionService>(MockBehavior.Strict);
            _maintenanceOperatorRepository = new Mock<IGenericRepository<MaintenanceOperator>>(MockBehavior.Strict);

            _category = new Category()
            {
                Id = 1,
                Name = "Sanitaria"
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

            _user = new Manager()
            {
                Buildings = [_building],
                Email = "manager@correo.com",
                Id = 1,
                Name = "Lucas",
                LastName = "Gonzalez",
                Password = "Lu.Go123!",
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
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object);

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
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object);

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
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object);

            _ticketService.CreateTicket(ticket);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void TestCreateTicketWithInvalidApartment()
        {
            var ticket = new Ticket()
            {
                Category = new Category() { Id = 1 },
                Apartment = new Apartment() { Id = 2 },
                Description = "Perdida de agua",
            };
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);
            _categoryRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(_category);
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object);

            _ticketService.CreateTicket(ticket);
        }

        [TestMethod]
        public void TestAssignTicket()
        {
            var ticket = new Ticket()
            {
                Id = 1,
                Category = _category,
                Apartment = _apartment,
                Description = "Perdida de agua",
            };
            _maintenance = new MaintenanceOperator()
            {
                Id = 2,
                Email = "mantenimiento@correo.com",
                Password = "Pass123.!",
                Name = "Rodrigo",
                LastName = "Rodriguez",
                Building = _building
            };
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);
            _ticketRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(ticket);
            _ticketRepository.Setup(r => r.Update(It.IsAny<Ticket>())).Verifiable();
            _maintenanceOperatorRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(_maintenance);
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object);

            var result = _ticketService.AssignTicket(1, 2);

            _sessionService.VerifyAll();
            _ticketRepository.VerifyAll();
            _maintenanceOperatorRepository.VerifyAll();
            Assert.AreEqual(result.AssignedTo, _maintenance);
        }

        [TestMethod]
        public void TestAssignNonExistentTicket()
        {
            Ticket ticket = null;
            _maintenance = new MaintenanceOperator()
            {
                Id = 2,
                Email = "mantenimiento@correo.com",
                Password = "Pass123.!",
                Name = "Rodrigo",
                LastName = "Rodriguez",
                Building = _building
            };
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);
            _ticketRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(ticket);
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object);

            var result = _ticketService.AssignTicket(100, 2);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestAssignInvalidTicket()
        {
            var apartment = new Apartment()
            {
                Id = 10,
                Bathrooms = 1,
                DoorNumber = 2,
                Floor = 2,
                HasTerrace = false,
                Owner = new Owner(),
                Rooms = 1
            };
            var ticket = new Ticket()
            {
                Id = 10,
                Category = _category,
                Apartment = apartment,
                Description = "Perdida de agua",
            };
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);
            _ticketRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(ticket);
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object);

            var result = _ticketService.AssignTicket(100, 2);

            Assert.IsNull(result);
        }


        [TestMethod]
        public void TestAssignTicketToNonExistentOperator()
        {
            var ticket = new Ticket()
            {
                Id = 1,
                Category = _category,
                Apartment = _apartment,
                Description = "Perdida de agua",
            };
            MaintenanceOperator maintenance = null;
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);
            _ticketRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(ticket);
            _maintenanceOperatorRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(maintenance);
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object);

            var result = _ticketService.AssignTicket(1, 99);

            _sessionService.VerifyAll();
            _ticketRepository.VerifyAll();
            _maintenanceOperatorRepository.VerifyAll();
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestAssignTicketToInvalidOperator()
        {
            var ticket = new Ticket()
            {
                Id = 1,
                Category = _category,
                Apartment = _apartment,
                Description = "Perdida de agua",
            };
            _maintenance = new MaintenanceOperator()
            {
                Id = 2,
                Email = "mantenimiento@correo.com",
                Password = "Pass123.!",
                Name = "Rodrigo",
                LastName = "Rodriguez",
                Building = new Building()
                {
                    Id = 80
                }
            };
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);
            _ticketRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(ticket);
            _maintenanceOperatorRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(_maintenance);
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object);

            var result = _ticketService.AssignTicket(1, 2);

            _sessionService.VerifyAll();
            _ticketRepository.VerifyAll();
            _maintenanceOperatorRepository.VerifyAll();
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestAssignNotOpenTicket()
        {
            var ticket = new Ticket()
            {
                Id = 1,
                Category = _category,
                Apartment = _apartment,
                Description = "Perdida de agua",
                Status = Domain.DataTypes.Status.InProgress
            };
            _maintenance = new MaintenanceOperator()
            {
                Id = 2,
                Email = "mantenimiento@correo.com",
                Password = "Pass123.!",
                Name = "Rodrigo",
                LastName = "Rodriguez",
                Building = _building
            };
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);
            _ticketRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(ticket);
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object);

            var result = _ticketService.AssignTicket(1, 2);

            _sessionService.VerifyAll();
            _ticketRepository.VerifyAll();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestStartTicket()
        {
            _maintenance = new MaintenanceOperator()
            {
                Id = 2,
                Email = "mantenimiento@correo.com",
                Password = "Pass123.!",
                Name = "Rodrigo",
                LastName = "Rodriguez",
                Building = _building
            };
            var ticket = new Ticket()
            {
                Id = 1,
                Category = _category,
                Apartment = _apartment,
                Description = "Perdida de agua",
                AssignedTo = _maintenance,
                Status = Domain.DataTypes.Status.Open,
                CreatedBy = _user
            };
            _ticketRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(ticket);
            _ticketRepository.Setup(r => r.Update(It.IsAny<Ticket>())).Verifiable();
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_maintenance);
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object);

            var result = _ticketService.StartTicket(1);

            _sessionService.VerifyAll();
            _ticketRepository.VerifyAll();

            Assert.AreEqual(result, ticket);
            Assert.AreEqual(result.Status, Domain.DataTypes.Status.InProgress);
            Assert.IsNotNull(result.AttentionDate);
        }

        [TestMethod]
        public void TestStartNonExistentTicket()
        {
            _maintenance = new MaintenanceOperator()
            {
                Id = 2,
                Email = "mantenimiento@correo.com",
                Password = "Pass123.!",
                Name = "Rodrigo",
                LastName = "Rodriguez",
                Building = _building
            };
            Ticket ticket = null;
            _ticketRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(ticket);
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_maintenance);
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object);

            var result = _ticketService.StartTicket(100);

            _sessionService.VerifyAll();
            _ticketRepository.VerifyAll();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestStartInvalidTicket()
        {
            _maintenance = new MaintenanceOperator()
            {
                Id = 2,
                Email = "mantenimiento@correo.com",
                Password = "Pass123.!",
                Name = "Rodrigo",
                LastName = "Rodriguez",
                Building = _building
            };
            var ticket = new Ticket()
            {
                Id = 1,
                Category = _category,
                Apartment = _apartment,
                Description = "Perdida de agua",
                AssignedTo = new MaintenanceOperator(),
                Status = Domain.DataTypes.Status.Open,
                CreatedBy = _user
            };
            _ticketRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(ticket);
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_maintenance);
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object);

            var result = _ticketService.StartTicket(1);

            _sessionService.VerifyAll();
            _ticketRepository.VerifyAll();

            Assert.IsNull(result);
        }
    }
}
