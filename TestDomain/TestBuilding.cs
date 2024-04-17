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

    [TestMethod]
    public void TestApartment()
    {
        Apartment apartment = new Apartment();
        building.Apartment = apartment;
        Assert.AreEqual(apartment, building.Apartment);
    }

    [TestMethod]
    [ExpectedException(typeof(EmptyFieldException))]
    public void TestInvalidApartment()
    {
        building.Apartment = null;
    }

    [TestMethod]
    public void TestConstructionCompany()
    {
        ConstructionCompany constructionCompany = new ConstructionCompany();
        building.ConstructionCompany = constructionCompany;
        Assert.AreEqual(constructionCompany, building.ConstructionCompany);
    }

    [TestMethod]
    [ExpectedException(typeof(EmptyFieldException))]
    public void TestInvalidConstructionCompany()
    {
        building.ConstructionCompany = null;
    }

    [TestMethod]
    public void TestModifyConstructionCompany()
    {
        ConstructionCompany constructionCompany = new ConstructionCompany();
        building.ConstructionCompany = constructionCompany;
        ConstructionCompany newConstructionCompany = new ConstructionCompany();
        building.ConstructionCompany = newConstructionCompany;
        Assert.AreEqual(newConstructionCompany, building.ConstructionCompany);
    }
}