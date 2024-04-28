using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace TestDataAccess
{
    [TestClass]
    public class TestSessionRepository
    {
        private BuildingAdminContext _adminContext;
        private SqliteConnection _connection;
        private SessionRepository _repository;

        private Session _session1;
        private Session _session2;

        [TestInitialize]
        public void Setup()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<BuildingAdminContext>()
                .UseSqlite(_connection)
                .Options;

            _adminContext = new BuildingAdminContext(options);
            _adminContext.Database.EnsureCreated();

            _repository = new SessionRepository(_adminContext);

            _session1 = new Session()
            {
                Id = 1,
                User = new Administrator()
            };

            _session2 = new Session()
            {
                Id = 2,
                User = new Manager()
            };

            _adminContext.Sessions.Add(_session1);
            _adminContext.Sessions.Add(_session2);
            _adminContext.SaveChanges();

        }


        [TestMethod]
        public void TestGetAll()
        {
            var sessions = _repository.GetAll<Session>();
            Assert.AreEqual(sessions.Count(), 2);
        }

        [TestMethod]
        public void TestGetSessionById()
        {
            var session = _repository.Get(2);
            Assert.AreEqual(session, _session2);
        }

        [TestMethod]
        public void TestInsert()
        {
            var session = new Session()
            {
                Id = 3,
                User = new Administrator()
            };
            _repository.Insert(session);

            CollectionAssert.Contains(_adminContext.Sessions.ToList(), session);
        }

        [TestMethod]
        public void TestGetNotFound()
        {
            var session = _repository.Get(5);

            Assert.IsNull(session);
        }

        [TestMethod]
        public void TestUpdate()
        {
            var session = _adminContext.Sessions.Find(2);
            var newUser = new Administrator();

            session.User = newUser;
            _repository.Update(session);

            Assert.AreEqual(session.User, _adminContext.Sessions.Find(2).User);
        }

        [TestMethod]
        public void TestDelete()
        {
            var session = _adminContext.Sessions.Find(2);
            
            _repository.Delete(session);

            CollectionAssert.DoesNotContain(_adminContext.Sessions.ToList(), session);
        }

        [TestMethod]
        public void TestCheckConnection()
        {
            bool connection = _repository.CheckConnection();

            Assert.IsTrue(connection);
        }

        [TestMethod]
        public void TestGetSessionByToken()
        {
            var token = _adminContext.Sessions.Find(2).Token;
            var session = _repository.GetByToken(token);

            Assert.AreEqual(session, _session2);
        }

    }
}
