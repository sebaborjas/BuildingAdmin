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

        [TestMethod]
        public void TestDoorNumber()
        {
            department = new Department();
            department.DoorNumber = 1;
            Assert.AreEqual(1, department.DoorNumber);
        }

    }
}
