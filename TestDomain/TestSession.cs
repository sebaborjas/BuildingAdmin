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
        private Session _session;

        [TestInitialize]
        public void Setup()
        {
            _session = new Session();
        }

        [TestMethod]
        public void TestId()
        {
            _session.Id = 10;
            Assert.AreEqual(10, _session.Id);
        }

        [TestMethod]
        public void TestUser() { 
            var user = new Administrator();
            _session.User = user;
            Assert.AreEqual(_session.User, user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullUser()
        {
            Administrator user = null;
            _session.User = user;
        }
    }
}
