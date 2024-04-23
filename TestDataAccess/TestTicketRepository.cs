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

        private List<Ticket> _tickets;

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

            LoadData();
        }

        [TestMethod]
        public void TestGetAllTickets()
        {
            var retrievedTickets = _repository.GetAll<Ticket>();
            CollectionAssert.AreEqual(_tickets, retrievedTickets.ToList());
        }

        [TestMethod]
        public void TestGetTicket()
        {
            var firstTicket = _context.Tickets.Find(1);

            var retrievedTicket = _repository.Get(1);
            Assert.AreEqual(firstTicket, retrievedTicket);
        }

        private void LoadData()
        {
            _tickets = new List<Ticket>
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
            _context.Tickets.AddRange(_tickets);
            _context.SaveChanges();
        }
    }
}
