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
        building = new Building()
        {
            Id = 1
        };
        Assert.AreEqual(1, building.Id);
    }

    [TestMethod]
    public void TestName()
    {
        building = new Building()
        {
            Name = "Building"
        };
        Assert.AreEqual("Building", building.Name);
    }

}