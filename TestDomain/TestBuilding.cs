using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exceptions;
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
        building.Id = -1;
    }

    [TestMethod]
    public void TestName()
    {
        building.Name = "Building";
        Assert.AreEqual("Building", building.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(EmptyFieldException))]
    public void TestNameEmpty()
    {
        building.Name = "";
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidDataException))]
    public void TestNameInvalid()
    {
        building.Name = "Building1";
    }

    [TestMethod]
    public void TestExpenses()
    {
        building.Expenses = 100;
        Assert.AreEqual(100, building.Expenses);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestInvalidExpenses()
    {
        building.Expenses = -1;
    }

    [TestMethod]
    public void TestModifyExpenses()
    {
        building.Expenses = 100;
        building.Expenses = 200;
        Assert.AreEqual(200, building.Expenses);
    }


}