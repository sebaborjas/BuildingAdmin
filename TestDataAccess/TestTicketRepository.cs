using DataAccess;
using Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDataAccess
{
    [TestClass]
    public class TestTicketRepository
    {

        private BuildingAdminContext _context;

        private SqliteConnection _connection;

        private TicketRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<BuildingAdminContext>()
                .UseSqlite(_connection)
                .Options;

            _context = new BuildingAdminContext(options);
            _context.Database.EnsureCreated();

            _repository = new TicketRepository(_context);
        }

        [TestMethod]
        public void TestGetAllTickets()
        {
            var tickets = new List<Ticket>
            {
                new Ticket
                { 
                    Id = 1,
                    Apartment = new Apartment(),
                    AssignedTo = new MaintenanceOperator(),
                    Category = new Category(),
                    CreatedBy = new MaintenanceOperator(),
                    Description = "Descripcion ticket 2",
                    Status = Domain.DataTypes.Status.Open
                },
                new Ticket
                {
                    Id = 2,
                    Apartment = new Apartment(),
                    AssignedTo = new MaintenanceOperator(),
                    Category = new Category(),
                    CreatedBy = new MaintenanceOperator(),
                    Description = "Descripcion ticket 1",
                    Status = Domain.DataTypes.Status.Open
                }
            };
            _context.Tickets.AddRange(tickets);
            _context.SaveChanges();

            var retrievedTickets = _repository.GetAll<Ticket>();
            CollectionAssert.AreEqual(tickets, retrievedTickets.ToList());
        }

        [TestMethod]
        public void TestGetTicket()
        {
            var tickets = new List<Ticket>
            {
                new Ticket
                {
                    Id = 1,
                    Apartment = new Apartment(),
                    AssignedTo = new MaintenanceOperator(),
                    Category = new Category(),
                    CreatedBy = new MaintenanceOperator(),
                    Description = "Descripcion ticket 2",
                    Status = Domain.DataTypes.Status.Open
                },
                new Ticket
                {
                    Id = 2,
                    Apartment = new Apartment(),
                    AssignedTo = new MaintenanceOperator(),
                    Category = new Category(),
                    CreatedBy = new MaintenanceOperator(),
                    Description = "Descripcion ticket 1",
                    Status = Domain.DataTypes.Status.Open
                }
            };
            _context.Tickets.AddRange(tickets);
            _context.SaveChanges();

            var firstTicket = _context.Tickets.Find(1);

            var retrievedTicket = _repository.Get(1);
            Assert.AreEqual(firstTicket, retrievedTicket);
        }
    }
}
