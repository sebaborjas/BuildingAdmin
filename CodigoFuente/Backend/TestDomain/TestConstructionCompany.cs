using Domain;
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
        ConstructionCompany.Name = "ConstructionCompany";
        Assert.AreEqual("ConstructionCompany", ConstructionCompany.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidDataException))]
    public void TestInvalidName()
    {
        ConstructionCompany.Name = "ConstructionCompany123";
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
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

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestNameWithMoreThan100Characters()
    {
        ConstructionCompany.Name = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
    }

    [TestMethod]
    public void TestEqualsWithOtherObject()
    {
        ConstructionCompany.Id = 1;
        ConstructionCompany.Equals(new Object());
    }
}