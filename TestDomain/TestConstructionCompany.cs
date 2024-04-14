using Domain;
using Exceptions;
namespace TestDomain;

[TestClass]
public class TestConstructionCompany
{
    ConstructionCompany ConstructionCompany;

    [TestInitialize]
    public void TestInitialize()
    {
        ConstructionCompany = new ConstructionCompany();
    }

    [TestMethod]
    public void TestName()
    {
        ConstructionCompany.Name = "ConstructionCompany1";
        Assert.AreEqual("ConstructionCompany1", ConstructionCompany.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(EmptyFieldException))]
    public void TestEmptyName()
    {
        ConstructionCompany.Name = "";
    }

    [TestMethod]
    public void TestId()
    {
        ConstructionCompany.Id = 1;
        Assert.AreEqual(1, ConstructionCompany.Id);
    }
}