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
        }
    }
}
