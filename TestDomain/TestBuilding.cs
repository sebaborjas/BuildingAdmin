using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace TestDomain;

[TestClass]
public class TestBuilding
{
    private Building building;

    [TestMethod]
    public void TestId()
    {
        building = new Building();
        building.Id = 1;
        Assert.AreEqual(1, building.Id);
    }

}