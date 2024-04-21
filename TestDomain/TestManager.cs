using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Exceptions;

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
        [ExpectedException(typeof(EmptyFieldException))]
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
        [ExpectedException(typeof(EmptyFieldException))]
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
        [ExpectedException(typeof(EmptyFieldException))]
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

    }
}
