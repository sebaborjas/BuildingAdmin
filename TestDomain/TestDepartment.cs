using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace TestDomain
{
    [TestClass]
    public  class TestDepartment
    {
        private Department department;

        [TestInitialize]
        public void Setup()
        {
            department = new Department();
        }

        [TestMethod]
        public void TestId()
        {
            department.Id = 1;
            Assert.AreEqual(1, department.Id);
        }

        [TestMethod]
        public void TestModifyId()
        {
            department.Id = 1;
            department.Id = 2;
            Assert.AreEqual(2, department.Id);
        }

        [TestMethod]
        public void TestDoorNumber()
        {
           department.DoorNumber = 1; 
           Assert.AreEqual(1, department.DoorNumber);
        }

        [TestMethod]
        public void TestModifyDoorNumber()
        {
            department.DoorNumber = 1;
            department.DoorNumber = 2;
            Assert.AreEqual(2, department.DoorNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void TestInvalidDoorNumber()
        {
            department.DoorNumber = -1;
        }

        [TestMethod]
        public void TestRooms()
        {
            department.Rooms = 3;
            Assert.AreEqual(3, department.Rooms);
        }

        [TestMethod]
        public void TestModifyRooms()
        {
            department.Rooms = 3;
            department.Rooms = 2;
            Assert.AreEqual(2, department.Rooms);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void TestInvalidRooms()
        {
            department.Rooms = -1;
        }

        [TestMethod]
        public void TestBathrooms()
        {
            department.Bathrooms = 1;
            Assert.AreEqual(1, department.Bathrooms);
        }

        [TestMethod]
        public void TestModifyBathrooms()
        {
            department.Bathrooms = 1;
            department.Bathrooms = 2;
            Assert.AreEqual(2, department.Bathrooms);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void TestInvalidBathrooms()
        {
            department.Bathrooms = -1;
        }

        [TestMethod]
        public void TestFloor()
        {
            department.Floor = 1;
            Assert.AreEqual(1, department.Floor);
        }

        [TestMethod]
        public void TestModifyFloor()
        {
            department.Floor = 1;
            department.Floor = 2;
            Assert.AreEqual(2, department.Floor);
        }

        [TestMethod]
        public void TestHasTerrace()
        {
            department.HasTerrace = true;
            Assert.IsTrue(department.HasTerrace);
        }

        [TestMethod]
        public void TestModifyHasTerrace()
        {
            department.HasTerrace = true;
            department.HasTerrace = false;
            Assert.IsFalse(department.HasTerrace);
        }

        [TestMethod]
        public void TestOwner()
        {
            Owner owner = new Owner();
            department.Owner = owner;
            Assert.AreEqual(owner, department.Owner);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void TestInvalidOwner()
        {
            department.Owner = null;
        }

    }
}
