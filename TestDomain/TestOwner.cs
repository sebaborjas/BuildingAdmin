using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace TestDomain
{
    [TestClass]
    public class TestOwner
    {
        [TestMethod]
        public void TestName()
        {
            var owner = new Owner();
            owner.Name = "Norberto";
            Assert.AreEqual("Norberto", owner.Name);
        }

        [TestMethod]
        public void TestModifyName()
        {
            var owner = new Owner();
            owner.Name = "Norberto";
            owner.Name = "Magela";
            Assert.AreEqual("Magela", owner.Name);
        }
    }
}
