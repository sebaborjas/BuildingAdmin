﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Domain;
using DataAccess;

namespace TestDataAccess
{
    [TestClass]
    public class TestManagerRepository
    {
        private BuildingAdminContext _context;
        private SqliteConnection _connection;
        private ManagerRepository _repository;
        private ConstructionCompany constructionCompany;
        private Building building1;
        private Building building2;


        [TestInitialize]
        public void SetUp()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<BuildingAdminContext>()
                .UseSqlite(_connection)
                .Options;

            _context = new BuildingAdminContext(options);
            _context.Database.EnsureCreated();

            _repository = new ManagerRepository(_context);

            constructionCompany = new ConstructionCompany
            {
                Id = 1,
                Name = "Constructora"
            };

            building1 = new Building
            {
                Id = 1,
                Name = "Edificio Principal",
                Address = "Calle, 0000, esquina",
                Expenses = 1000,
                Location = "1.000, 1.000",
                ConstructionCompany = constructionCompany,
                Tickets = new List<Ticket>(),
                Apartments = new List<Apartment>(),
            };

            building2 = new Building
            {
                Id = 2,
                Name = "Edificio Secundario",
                Address = "Calle, 1111, esquina",
                Expenses = 2000,
                Location = "2.000, 2.000",
                ConstructionCompany = constructionCompany,
                Tickets = new List<Ticket>(),
                Apartments = new List<Apartment>(),
            };
        }

        private List<Manager> Data()
        {
            List<Manager> list = new List<Manager>
            {
                new Manager
                {
                    Id = 1,
                    Name = "Seba Borjas",
                    Email = "seba@test.com",
                    Password = "SebaB.1234",
                    Buildings = new List<Building> { building1, building2}
                },

                new Manager
                {
                    Id = 2,
                    Name = "Rodri Conze",
                    Email = "rodri@test.com",
                    Password = "Conze.1234",
                    Buildings = new List<Building> { building2 }
                },

                new Manager
                {
                    Id = 3,
                    Name = "Agus Martinez",
                    Email = "agus@test.com",
                    Password = "AgusM.1234",
                    Buildings = new List<Building> { building1 }
                }
            };

            return list;
        }

        public void LoadConext(List<Manager> list)
        {
            _context.Managers.AddRange(list);
            _context.SaveChanges();
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            _connection.Close();
        }

        [TestMethod]
        public void TestInsert()
        {
            Manager manager = new Manager
            {
                Id = 4,
                Name = "Luis Perez",
                Email = "luis@test.com",
                Password = "Prueba.1234",
                Buildings = new List<Building>()
            };

            _repository.Insert(manager);

            Assert.AreEqual(manager, _context.Managers.Find(4));
        }

        [TestMethod]
        public void TestGet()
        {
            var managerList = Data();
            LoadConext(managerList);

            var getManager = _repository.Get(1);

            Assert.AreEqual(getManager, managerList[0]);
        }

        [TestMethod]
        public void TestGetManagerNotFound()
        {
            var managerList = Data();
            LoadConext(managerList);

            var getManager = _repository.Get(4);

            Assert.IsNull(getManager);
        }

        [TestMethod]
        public void TestGetAll()
        {
            var managerList = Data();
            LoadConext(managerList);

            var getAllManagers = _repository.GetAll<Manager>().ToList();

            CollectionAssert.AreEqual(managerList, getAllManagers);
        }

        [TestMethod]
        public void TestGetAllNoList()
        {
            var getManagers = _repository.GetAll<Manager>().ToList();

            int count = getManagers.Count;

            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void TestUpdate()
        {
            var managerList = Data();
            LoadConext(managerList);

            var manager = _context.Managers.Find(1);

            string nuevoEmail = "juanperez@test.com";

            manager.Email = nuevoEmail;

            _repository.Update(manager);

            Assert.AreEqual(nuevoEmail, _context.Managers.Find(1).Email);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateNotFound()
        {
            var managerList = Data();
            LoadConext(managerList);

            var manager = _context.Managers.Find(4);

            _repository.Update(manager);
        }

        [TestMethod]
        public void TestDelete()
        {
            var managerList = Data();
            LoadConext(managerList);

            var manager = _context.Managers.Find(1);

            _repository.Delete(manager);

            Assert.IsNull(_context.Managers.Find(1));

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDeleteNotFound()
        {
            var managerList = Data();
            LoadConext(managerList);

            var manager = _context.Managers.Find(5);

            _repository.Delete(manager);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDeleteEmptyList()
        {
            var manager = _context.Managers.Find(5);

            _repository.Delete(manager);
        }

        [TestMethod]
        public void TestSaveChanges()
        {
            var managerList = Data();
            LoadConext(managerList);

            var manager = _context.Managers.Find(3);

            _repository.Delete(manager);

            _repository.Save();

            Assert.IsNull(_context.Managers.Find(3));
        }

        [TestMethod]
        public void TestCheckConnection()
        {
            bool connection = _repository.CheckConnection();

            Assert.IsTrue(connection);
        }
    }
}
