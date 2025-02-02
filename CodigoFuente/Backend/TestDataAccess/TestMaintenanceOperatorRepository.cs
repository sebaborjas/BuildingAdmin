﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Domain;
using DataAccess;

namespace TestDataAccess
{
    [TestClass]
    public class TestMaintenanceOperatorRepository
    {
        private BuildingAdminContext _context;
        private SqliteConnection _connection;
        private MaintenanceOperatorRepository _repository;
        private ConstructionCompany constructionCompany;
        private Building building;


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

            _repository = new MaintenanceOperatorRepository(_context);

            constructionCompany = new ConstructionCompany
            {
                Id = 1,
                Name = "Constructora SA"
            };

            building = new Building
            {
                Id = 1,
                Name = "Edificio central",
                Address = "Calle, 2222, esquina",
                Expenses = 1000,
                Location = "1.000, 1.000",
                ConstructionCompany = constructionCompany,
                Tickets = new List<Ticket>(),
                Apartments = new List<Apartment>(),
            };
        }

        private List<MaintenanceOperator> Data()
        {
            List<MaintenanceOperator> list = new List<MaintenanceOperator>
            {
                new MaintenanceOperator
                {
                    Id = 1,
                    Name = "Seba",
                    LastName = "Borjas",
                    Email = "seba@borjas.com",
                    Buildings = [building]
                },
                new MaintenanceOperator
                {
                    Id = 2,
                    Name = "Rodri",
                    LastName = "Conze",
                    Email = "rodri@conze.com",
                    Buildings = [building]
                }

            };
            return list;

        }

        private void LoadConext(List<MaintenanceOperator> list)
        {
            _context.MaintenanceOperators.AddRange(list);
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
            MaintenanceOperator maintenanceOperator = new MaintenanceOperator
            {
                Id = 3,
                Name = "Juan",
                LastName = "Perez",
                Email = "juan@perez.com",
                Password = "Juan.1234",
                Buildings = [building]
            };

            _repository.Insert(maintenanceOperator);

            Assert.AreEqual(maintenanceOperator, _context.MaintenanceOperators.Find(3));
        }

        [TestMethod]
        public void TestGet()
        {
            var maintenanceOperators = Data();
            LoadConext(maintenanceOperators);

            var maintenanceOperator = _repository.Get(1);

            Assert.AreEqual(maintenanceOperators[0], maintenanceOperator);
        }

        [TestMethod]
        public void TestGetAll()
        {
            var maintenanceOperators = Data();
            LoadConext(maintenanceOperators);

            var maintenanceOperatorsList = _repository.GetAll<MaintenanceOperator>().ToList();

            CollectionAssert.AreEqual(maintenanceOperators, maintenanceOperatorsList);
        }

        [TestMethod]
        public void TestGetNotFound()
        {
            var maintenanceOperators = Data();
            LoadConext(maintenanceOperators);

            var maintenanceOperator = _repository.Get(6);

            Assert.IsNull(maintenanceOperator);
        }

        [TestMethod]
        public void TestUpdate()
        {
            var maintenanceOperators = Data();
            LoadConext(maintenanceOperators);

            var maintenanceOperator = _repository.Get(1);

            string newName = "Sebastian";
            maintenanceOperator.Name = newName;

            _repository.Update(maintenanceOperator);

            Assert.AreEqual(newName, _context.MaintenanceOperators.Find(1).Name);
        }

        [TestMethod]
        public void TestDelete()
        {
            var maintenanceOperators = Data();
            LoadConext(maintenanceOperators);

            var maintenanceOperator = _context.MaintenanceOperators.Find(1);

            _repository.Delete(maintenanceOperator);

            Assert.IsNull(_context.MaintenanceOperators.Find(1));
        }

        [TestMethod]
        public void TestGetAllNoList()
        {
            var maintenanceOperators = _repository.GetAll<MaintenanceOperator>().ToList();

            int count = maintenanceOperators.Count;

            Assert.AreEqual(0, count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDeleteNotFound()
        {
            var maintenanceOperators = Data();
            LoadConext(maintenanceOperators);

            var maintenanceOperator = _context.MaintenanceOperators.Find(4);
            _repository.Delete(maintenanceOperator);
        }

        [TestMethod]
        public void TestSaveChanges()
        {
            var maintenanceOperators = Data();
            LoadConext(maintenanceOperators);

            var maintenanceOperator = _context.MaintenanceOperators.Find(1);


            _repository.Delete(maintenanceOperator);
            _repository.Save();

            Assert.IsNull(_context.MaintenanceOperators.Find(1));
        }

        [TestMethod]
        public void TestCheckConnection()
        {
            bool connection = _repository.CheckConnection();
            Assert.IsTrue(connection);
        }
    }
}

