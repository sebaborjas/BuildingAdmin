using Domain;
using Exceptions;
namespace TestDomain;

[TestClass]
public class TestCategory
{
    Category Category;

    [TestInitialize]
    public void TestInitialize()
    {
        Category = new Category();
    }

    [TestMethod]
    public void TestName()
    {
        Category.Name = "Category";
        Assert.AreEqual("Category", Category.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(EmptyFieldException))]
    public void TestEmptyName()
    {
        Category.Name = "";
    }

    [TestMethod]
    [ExpectedException(typeof(EmptyFieldException))]
    public void TestNullName()
    {
        Category.Name = null;
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidDataException))]
    public void TestInvalidNameWithNumbers()
    {
        Category.Name = "Category1";
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidDataException))]
    public void TestInvalidNameWithSpecialCharacters()
    {
        Category.Name = "Category!";
    }
}