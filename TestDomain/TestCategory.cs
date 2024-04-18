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

}