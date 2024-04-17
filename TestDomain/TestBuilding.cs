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
    public void TestApartments()
    {
        List<Apartment> apartments = new List<Apartment>();
        building.Apartments = apartments;
        Assert.AreEqual(apartments, building.Apartments);
    }

    [TestMethod]
    public void TestApartmentsCount()
    {
        Assert.AreEqual(0, building.Apartments.Count);
    }

    [TestMethod]
    public void TestAddApartment()
    {
        Apartment apartment = new Apartment();
        building.Apartments.Add(apartment);
        Assert.AreEqual(1, building.Apartments.Count);
    }

    [TestMethod]
    public void TestRemoveApartment()
    {
        Apartment apartment = new Apartment();
        building.Apartments.Add(apartment);
        building.Apartments.Remove(apartment);
        Assert.AreEqual(0, building.Apartments.Count);
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

    [TestMethod]
    public void TestLocation()
    {
        building.Location = "10.123456, 10.123456";
        Assert.AreEqual("10.123456, 10.123456", building.Location);
    }

    [TestMethod]
    [ExpectedException(typeof(EmptyFieldException))]
    public void TestEmptyLocation()
    {
        building.Location = "";
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidDataException))]
    public void TestInvalidLocation()
    {
        building.Location = "location1";
    }

    [TestMethod]
    public void TestAddress()
    {
        building.Address = "Calle 123";
        Assert.AreEqual("Calle 123", building.Address);
    }

    [TestMethod]
    [ExpectedException(typeof(EmptyFieldException))]
    public void TestEmptyAddress()
    {
        building.Address = "";
    }

}