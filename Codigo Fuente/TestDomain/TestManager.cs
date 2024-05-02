using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Exceptions;

namespace TestDomain
{
    [TestClass]
    public class TestManager
    {
        private Manager manager;

        [TestInitialize]
        public void setUp()
        {
            manager = new Manager();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EmptyName()
        {
            manager.Name = "";
        }

        [TestMethod]
        public void CorrectName()
        {
            manager.Name = "NewName";
            Assert.AreEqual("NewName", manager.Name);
        }

        [TestMethod]
        public void EmptyLastName()
        {
            manager.LastName = "";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EmptyEmail()
        {
            manager.Email = "";
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEmailFormatException))]
        public void WrongEmail()
        {
            manager.Email = "test";
        }

        [TestMethod]
        public void CorrectEmail()
        {
            string email = "prueba@test.com";
            manager.Email = email;
            Assert.AreEqual(email, manager.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EmptyPassword()
        {
            manager.Password = "";
        }

        [TestMethod]
        [ExpectedException(typeof(PasswordNotFollowPolicy))]
        public void WrongPasswordLength()
        {
            manager.Password = "123";
        }

        [TestMethod]
        [ExpectedException(typeof(PasswordNotFollowPolicy))]
        public void WrongPasswordFormat()
        {
            manager.Password = "password";
        }

        [TestMethod]
        [ExpectedException(typeof(PasswordNotFollowPolicy))]
        public void PasswordWithoutSpecialCharacter()
        {
            manager.Password = "Prueba123";
        }

        [TestMethod]
        [ExpectedException(typeof(PasswordNotFollowPolicy))]
        public void PasswordTooMuchLong()
        {
            manager.Password = "Prueba.123456789";
        }

        [TestMethod]
        public void CorrectPassword()
        {
            manager.Password = "Pru#eba.123$";
        }

        [TestMethod]
        public void TestBuildings()
        {
            List<Building> buildings = new List<Building>();
            manager.Buildings = buildings;
            Assert.AreEqual(buildings, manager.Buildings);
        }

        [TestMethod]
        public void TestBuildingEmpty()
        {
            Assert.AreEqual(0, manager.Buildings.Count);
        }

        [TestMethod]
        public void TestAddBuilding()
        {
            Building building = new Building();
            manager.Buildings.Add(building);
            Assert.AreEqual(1, manager.Buildings.Count);
        }

        [TestMethod]
        public void TestRemoveBuilding()
        {
            Building building = new Building();
            manager.Buildings.Add(building);
            manager.Buildings.Remove(building);
            Assert.AreEqual(0, manager.Buildings.Count);
        }

    }
}
