using Domain;
using IDataAccess;
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
        private Mock<IGenericRepository<Building>> _buildingRepository;
        private Mock<ISessionService> _sessionService;

        private Manager _user;
        private List<Building> _buildings;
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
            _buildingRepository = new Mock<IGenericRepository<Building>>(MockBehavior.Strict);

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

            _buildings = new List<Building>()
            {
                new Building(){
                    Id = 1,
                    Apartments = [_apartment],
                    Address = "Rivera, 1111, Soca",
                    ConstructionCompany = new ConstructionCompany(),
                    Expenses = 2000,
                    Location = "111,111",
                    Name = "Edificio Las Delicias"
                }
            };

            _user = new Manager()
            {
                Buildings = _buildings,
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
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);
            _ticketRepository.Setup(r => r.Insert(It.IsAny<Ticket>())).Verifiable();
            _categoryRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(_category);
            _buildingRepository.Setup(r => r.Update(It.IsAny<Building>())).Verifiable();
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object, _buildingRepository.Object);

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
            _buildingRepository.Setup(r => r.Update(It.IsAny<Building>())).Verifiable();
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object, _buildingRepository.Object);

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
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object, _buildingRepository.Object);

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
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object, _buildingRepository.Object);

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
                Buildings = _buildings
            };
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);
            _ticketRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(ticket);
            _ticketRepository.Setup(r => r.Update(It.IsAny<Ticket>())).Verifiable();
            _maintenanceOperatorRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(_maintenance);
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object, _buildingRepository.Object);

            var result = _ticketService.AssignTicket(1, 2);

            _sessionService.VerifyAll();
            _ticketRepository.VerifyAll();
            _maintenanceOperatorRepository.VerifyAll();
            Assert.AreEqual(result.IdOperatorAssigned, _maintenance.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
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
                Buildings = _buildings
            };
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);
            _ticketRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(ticket);
            _ticketService = _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object, _buildingRepository.Object);

            var result = _ticketService.AssignTicket(100, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
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
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object, _buildingRepository.Object);

            var result = _ticketService.AssignTicket(100, 2);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
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
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object, _buildingRepository.Object);

            var result = _ticketService.AssignTicket(1, 99);

            _sessionService.VerifyAll();
            _ticketRepository.VerifyAll();
            _maintenanceOperatorRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
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
                Buildings = new List<Building>()
                {
                    new Building() {
                        Id = 80
                    }
                }
            };
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);
            _ticketRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(ticket);
            _maintenanceOperatorRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(_maintenance);
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object, _buildingRepository.Object);

            var result = _ticketService.AssignTicket(1, 2);

            _sessionService.VerifyAll();
            _ticketRepository.VerifyAll();
            _maintenanceOperatorRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
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
                Buildings = _buildings
            };
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);
            _ticketRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(ticket);
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object, _buildingRepository.Object);

            var result = _ticketService.AssignTicket(1, 2);

            _sessionService.VerifyAll();
            _ticketRepository.VerifyAll();
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
                Buildings = _buildings
            };

            var expectedTicket = new Ticket()
            {
                Id = 1,
                Category = _category,
                Apartment = _apartment,
                Description = "Perdida de agua",
                IdOperatorAssigned = _maintenance.Id,
                Status = Domain.DataTypes.Status.InProgress,
                CreatedBy = _user
            };

            var actualTicket = new Ticket()
            {
                Id = 1,
                Category = _category,
                Apartment = _apartment,
                Description = "Perdida de agua",
                IdOperatorAssigned = _maintenance.Id,
                Status = Domain.DataTypes.Status.Open,
                CreatedBy = _user
            };

            _ticketRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(actualTicket);
            _ticketRepository.Setup(r => r.Update(It.IsAny<Ticket>())).Verifiable();
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_maintenance);

            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object, _buildingRepository.Object);

            var result = _ticketService.StartTicket(1);

            _sessionService.VerifyAll();
            _ticketRepository.VerifyAll();

            Assert.AreEqual(expectedTicket, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void TestStartNonExistentTicket()
        {
            _maintenance = new MaintenanceOperator()
            {
                Id = 2,
                Email = "mantenimiento@correo.com",
                Password = "Pass123.!",
                Name = "Rodrigo",
                LastName = "Rodriguez",
                Buildings = _buildings
            };
            Ticket ticket = null;
            _ticketRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(ticket);
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_maintenance);
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object, _buildingRepository.Object);

            var result = _ticketService.StartTicket(100);

            _sessionService.VerifyAll();
            _ticketRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void TestStartInvalidTicket()
        {
            _maintenance = new MaintenanceOperator()
            {
                Id = 2,
                Email = "mantenimiento@correo.com",
                Password = "Pass123.!",
                Name = "Rodrigo",
                LastName = "Rodriguez",
                Buildings = _buildings
            };
            var ticket = new Ticket()
            {
                Id = 1,
                Category = _category,
                Apartment = _apartment,
                Description = "Perdida de agua",
                IdOperatorAssigned = 5,
                Status = Domain.DataTypes.Status.Open,
                CreatedBy = _user
            };
            _ticketRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(ticket);
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_maintenance);
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object, _buildingRepository.Object);

            var result = _ticketService.StartTicket(1);

            _sessionService.VerifyAll();
            _ticketRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void TestStartNotOpenTicket()
        {
            _maintenance = new MaintenanceOperator()
            {
                Id = 2,
                Email = "mantenimiento@correo.com",
                Password = "Pass123.!",
                Name = "Rodrigo",
                LastName = "Rodriguez",
                Buildings = _buildings
            };
            var ticket = new Ticket()
            {
                Id = 1,
                Category = _category,
                Apartment = _apartment,
                Description = "Perdida de agua",
                IdOperatorAssigned = _maintenance.Id,
                Status = Domain.DataTypes.Status.InProgress,
                CreatedBy = _user
            };
            _ticketRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(ticket);
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_maintenance);
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object, _buildingRepository.Object);

            var result = _ticketService.StartTicket(1);

            _sessionService.VerifyAll();
            _ticketRepository.VerifyAll();
        }


        [TestMethod]
        public void TestCompleteTicket()
        {
            _maintenance = new MaintenanceOperator()
            {
                Id = 2,
                Email = "mantenimiento@correo.com",
                Password = "Pass123.!",
                Name = "Rodrigo",
                LastName = "Rodriguez",
                Buildings = _buildings
            };
            var ticket = new Ticket()
            {
                Id = 1,
                Category = _category,
                Apartment = _apartment,
                Description = "Perdida de agua",
                IdOperatorAssigned = _maintenance.Id,
                Status = Domain.DataTypes.Status.InProgress,
                CreatedBy = _user
            };
            _ticketRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(ticket);
            _ticketRepository.Setup(r => r.Update(It.IsAny<Ticket>())).Verifiable();
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_maintenance);
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object, _buildingRepository.Object);

            var result = _ticketService.CompleteTicket(1, 2000);

            Assert.IsTrue(result.Status == Domain.DataTypes.Status.Closed &&
                          result.ClosingDate != null &&
                          result.TotalCost == 2000);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void TestCompleteNonExistentTicket()
        {
            _maintenance = new MaintenanceOperator()
            {
                Id = 2,
                Email = "mantenimiento@correo.com",
                Password = "Pass123.!",
                Name = "Rodrigo",
                LastName = "Rodriguez",
                Buildings = _buildings
            };
            Ticket ticket = null;
            _ticketRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(ticket);
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_maintenance);
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object, _buildingRepository.Object);

            var result = _ticketService.CompleteTicket(100, 2000);

            _sessionService.VerifyAll();
            _ticketRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void TestCompleteInvalidTicket()
        {
            _maintenance = new MaintenanceOperator()
            {
                Id = 2,
                Email = "mantenimiento@correo.com",
                Password = "Pass123.!",
                Name = "Rodrigo",
                LastName = "Rodriguez",
                Buildings = _buildings
            };
            var ticket = new Ticket()
            {
                Id = 1,
                Category = _category,
                Apartment = _apartment,
                Description = "Perdida de agua",
                IdOperatorAssigned = 5,
                Status = Domain.DataTypes.Status.InProgress,
                CreatedBy = _user
            };
            _ticketRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(ticket);
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_maintenance);
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object, _buildingRepository.Object);

            var result = _ticketService.CompleteTicket(1, 2000);

            _sessionService.VerifyAll();
            _ticketRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void TestCompleteNotInProgressTicket()
        {
            _maintenance = new MaintenanceOperator()
            {
                Id = 2,
                Email = "mantenimiento@correo.com",
                Password = "Pass123.!",
                Name = "Rodrigo",
                LastName = "Rodriguez",
                Buildings = _buildings
            };
            var ticket = new Ticket()
            {
                Id = 1,
                Category = _category,
                Apartment = _apartment,
                Description = "Perdida de agua",
                IdOperatorAssigned = _maintenance.Id,
                Status = Domain.DataTypes.Status.Open,
                CreatedBy = _user
            };
            _ticketRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(ticket);
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_maintenance);
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object, _buildingRepository.Object);

            var result = _ticketService.CompleteTicket(1, 2000);

            _sessionService.VerifyAll();
            _ticketRepository.VerifyAll();
        }

        [TestMethod]
        public void TestGetTickets()
        {
            _maintenance = new MaintenanceOperator()
            {
                Id = 2,
                Email = "mantenimiento@correo.com",
                Password = "Pass123.!",
                Name = "Rodrigo",
                LastName = "Rodriguez",
                Buildings = _buildings
            };
            var ticket1 = new Ticket()
            {
                Id = 1,
                Category = _category,
                Apartment = _apartment,
                Description = "Perdida de agua",
                IdOperatorAssigned = _maintenance.Id,
                Status = Domain.DataTypes.Status.Open,
                CreatedBy = _user
            };
            var ticket2 = new Ticket()
            {
                Id = 2,
                Category = _category,
                Apartment = _apartment,
                Description = "Perdida de agua",
                IdOperatorAssigned = _maintenance.Id,
                Status = Domain.DataTypes.Status.Open,
                CreatedBy = _user
            };
            var ticket3 = new Ticket()
            {
                Id = 3,
                Category = _category,
                Apartment = new Apartment(),
                Description = "Perdida de agua",
                IdOperatorAssigned = _maintenance.Id,
                Status = Domain.DataTypes.Status.Open,
                CreatedBy = new Manager()
            };
            var ticket4 = new Ticket()
            {
                Id = 4,
                Category = _category,
                Apartment = _apartment,
                Description = "Perdida de agua",
                IdOperatorAssigned = _maintenance.Id,
                Status = Domain.DataTypes.Status.Open,
                CreatedBy = _user
            };
            var allTickets = new List<Ticket>
            {
                ticket1, ticket2, ticket3, ticket4
            };
            _ticketRepository.Setup(r => r.GetAll<Ticket>()).Returns(allTickets);
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object, _buildingRepository.Object);

            var result = _ticketService.GetTickets(null);

            _sessionService.VerifyAll();
            _ticketRepository.VerifyAll();
            Assert.AreEqual(result.Count, 3);
        }


        [TestMethod]
        public void TestGetTicketsFiltered()
        {
            _maintenance = new MaintenanceOperator()
            {
                Id = 2,
                Email = "mantenimiento@correo.com",
                Password = "Pass123.!",
                Name = "Rodrigo",
                LastName = "Rodriguez",
                Buildings = _buildings
            };
            var ticket1 = new Ticket()
            {
                Id = 1,
                Category = _category,
                Apartment = _apartment,
                Description = "Perdida de agua",
                IdOperatorAssigned = _maintenance.Id,
                Status = Domain.DataTypes.Status.Open,
                CreatedBy = _user
            };
            var ticket2 = new Ticket()
            {
                Id = 2,
                Category = new Category() { Id = 5, Name = "Otra categoria" },
                Apartment = _apartment,
                Description = "Perdida de agua",
                IdOperatorAssigned = _maintenance.Id,
                Status = Domain.DataTypes.Status.Open,
                CreatedBy = _user
            };
            var ticket3 = new Ticket()
            {
                Id = 3,
                Category = _category,
                Apartment = new Apartment(),
                Description = "Perdida de agua",
                IdOperatorAssigned = _maintenance.Id,
                Status = Domain.DataTypes.Status.Open,
                CreatedBy = new Manager()
            };
            var ticket4 = new Ticket()
            {
                Id = 4,
                Category = _category,
                Apartment = _apartment,
                Description = "Perdida de agua",
                IdOperatorAssigned = _maintenance.Id,
                Status = Domain.DataTypes.Status.Open,
                CreatedBy = _user
            };
            var allTickets = new List<Ticket>
            {
                ticket1, ticket2, ticket3, ticket4
            };
            _ticketRepository.Setup(r => r.GetAll<Ticket>()).Returns(allTickets);
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object, _buildingRepository.Object);

            var result = _ticketService.GetTickets(_category.Name);

            _sessionService.VerifyAll();
            _ticketRepository.VerifyAll();
            Assert.AreEqual(result.Count, 2);
        }

        [TestMethod]
        public void TestGetTicketsEmptyList()
        {
            List<Ticket> allTickets = new List<Ticket>();
            _ticketRepository.Setup(r => r.GetAll<Ticket>()).Returns(allTickets);
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object, _buildingRepository.Object);

            var result = _ticketService.GetTickets();

            _sessionService.VerifyAll();
            _ticketRepository.VerifyAll();
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void TestCreateInvalidTicket()
        {
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object, _buildingRepository.Object);
            _ticketService.CreateTicket(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestCreateTicketError()
        {
            var ticket = new Ticket()
            {
                Category = new Category() { Id = 1 },
                Apartment = new Apartment() { Id = 1 },
                Description = "Perdida de agua",
            };
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);
            _ticketRepository.Setup(r => r.Insert(It.IsAny<Ticket>())).Throws(new Exception());
            _categoryRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(_category);
            _buildingRepository.Setup(r => r.Update(It.IsAny<Building>())).Verifiable();
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object, _buildingRepository.Object);

            var result = _ticketService.CreateTicket(ticket);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestAssignTicketError()
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
                Buildings = _buildings
            };
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);
            _ticketRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(ticket);
            _ticketRepository.Setup(r => r.Update(It.IsAny<Ticket>())).Throws(new Exception());
            _maintenanceOperatorRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(_maintenance);
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object, _buildingRepository.Object);

            var result = _ticketService.AssignTicket(1, 2);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestCompleteTicketError()
        {
            _maintenance = new MaintenanceOperator()
            {
                Id = 2,
                Email = "mantenimiento@correo.com",
                Password = "Pass123.!",
                Name = "Rodrigo",
                LastName = "Rodriguez",
                Buildings = _buildings
            };
            var ticket = new Ticket()
            {
                Id = 1,
                Category = _category,
                Apartment = _apartment,
                Description = "Perdida de agua",
                IdOperatorAssigned = _maintenance.Id,
                Status = Domain.DataTypes.Status.InProgress,
                CreatedBy = _user
            };
            _ticketRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(ticket);
            _ticketRepository.Setup(r => r.Update(It.IsAny<Ticket>())).Throws(new Exception());
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_maintenance);
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object, _buildingRepository.Object);

            var result = _ticketService.CompleteTicket(1, 2000);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGetTicketsError()
        {
            _ticketRepository.Setup(r => r.GetAll<Ticket>()).Throws(new Exception());
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object, _buildingRepository.Object);

            var result = _ticketService.GetTickets(null);
        }

        [TestMethod]
        public void TestGetAssignedTickets()
        {
            _maintenance = new MaintenanceOperator()
            {
                Id = 2,
                Email = "mantenimiento@correo.com",
                Password = "Pass123.!",
                Name = "Rodrigo",
                LastName = "Rodriguez",
                Buildings = _buildings
            };
            var ticket1 = new Ticket()
            {
                Id = 1,
                Category = _category,
                Apartment = _apartment,
                Description = "Perdida de agua",
                IdOperatorAssigned = _maintenance.Id,
                Status = Domain.DataTypes.Status.Open,
                CreatedBy = _user
            };
            var ticket2 = new Ticket()
            {
                Id = 2,
                Category = _category,
                Apartment = _apartment,
                Description = "Perdida de agua",
                IdOperatorAssigned = _maintenance.Id,
                Status = Domain.DataTypes.Status.Open,
                CreatedBy = _user
            };
            var ticket3 = new Ticket()
            {
                Id = 3,
                Category = _category,
                Apartment = new Apartment(),
                Description = "Perdida de agua",
                IdOperatorAssigned = _maintenance.Id,
                Status = Domain.DataTypes.Status.Open,
                CreatedBy = new Manager()
            };
            var ticket4 = new Ticket()
            {
                Id = 4,
                Category = _category,
                Apartment = _apartment,
                Description = "Perdida de agua",
                IdOperatorAssigned = 100,
                Status = Domain.DataTypes.Status.Open,
                CreatedBy = _user
            };
            var allTickets = new List<Ticket>
            {
                ticket1, ticket2, ticket3, ticket4
            };
            _ticketRepository.Setup(r => r.GetAll<Ticket>()).Returns(allTickets);
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_maintenance);
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object, _buildingRepository.Object);

            var result = _ticketService.GetAssignedTickets();

            _sessionService.VerifyAll();
            _ticketRepository.VerifyAll();
            Assert.AreEqual(result.Count, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGetAssignedTicketsError()
        {
            _maintenance = new MaintenanceOperator()
            {
                Id = 2,
                Email = "mantenimiento@correo.com",
                Password = "Pass123.!",
                Name = "Rodrigo",
                LastName = "Rodriguez",
                Buildings = _buildings
            };
            _ticketRepository.Setup(r => r.GetAll<Ticket>()).Throws(new Exception());
            _sessionService.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_maintenance);
            _ticketService = new TicketService(_ticketRepository.Object, _sessionService.Object, _categoryRepository.Object, _maintenanceOperatorRepository.Object, _buildingRepository.Object);

            var result = _ticketService.GetAssignedTickets();

        }
    }
}
