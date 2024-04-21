using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace TestDomain
{
    [TestClass]
    public class TestManager
    {

        [TestMethod]
        public void EmptyName()
        {
            Manager m = new Manager();
            string name = "";
            m.Name = name;
        }

    }
}
