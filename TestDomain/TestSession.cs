using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace TestDomain
{
    [TestClass]
    public class TestSession
    {

        [TestMethod]
        public void TestId()
        {
            var session = new Session();
            session.Id = 10;
            Assert.AreEqual(10, session.Id);
        }
    }
}
