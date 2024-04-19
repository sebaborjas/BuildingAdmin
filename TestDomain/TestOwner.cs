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
    public class TestOwner
    {

        Owner owner;

        [TestInitialize]
        public void Setup()
        {
            owner = new Owner();
        }

        [TestMethod]
        public void TestName()
        {
            owner.Name = "Norberto";
            Assert.AreEqual("Norberto", owner.Name);
        }

        [TestMethod]
        public void TestModifyName()
        {
            owner.Name = "Norberto";
            owner.Name = "Magela";
            Assert.AreEqual("Magela", owner.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyFieldException))]
        public void TestEmptyName()
        {
            owner.Name = String.Empty;
        }

        [TestMethod]
        public void TestLastName()
        {
            owner.LastName = "Rodriguez";
            Assert.AreEqual("Rodriguez", owner.LastName);
        }

        [TestMethod]
        public void TestModifyLastName()
        {
            owner.LastName = "Rodriguez";
            owner.LastName = "Gonzalez";
            Assert.AreEqual("Gonzalez", owner.LastName);
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyFieldException))]
        public void TestEmptyLastName()
        {
            owner.LastName = String.Empty;
        }
    }
}
