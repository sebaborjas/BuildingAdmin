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

        [TestMethod]
        [ExpectedException(typeof(EmptyFieldException))]
        public void EmptyName()
        {
            Manager m = new Manager();
            string name = "";
            m.Name = name;
        }

    }
}
