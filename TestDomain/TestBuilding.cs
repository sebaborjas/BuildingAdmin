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

    [TestInitialize]
    public void TestInitialize()
    {
        building = new Building();
    }

    [TestMethod]
    public void TestId()
    {
        building.Id = 1;
        Assert.AreEqual(1, building.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestIdNegative()
    {
        building.Id = 0;
    }

    [TestMethod]
    public void TestName()
    {
        building.Name = "Building";
        Assert.AreEqual("Building", building.Name);
    }

}