﻿using Domain;
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
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestEmptyName()
    {
        Category.Name = "";
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
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

    [TestMethod]
    [ExpectedException(typeof(InvalidDataException))]
    public void TestInvalidNameWithLessThan3Characters()
    {
        Category.Name = "Ca";
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidDataException))]
    public void TestInvalidNameWithMoreThan20Characters()
    {
        Category.Name = "Nombre de categoría muy largo largo";
    }

    [TestMethod]
    public void TestId()
    {
        Category.Id = 1;
        Assert.AreEqual(1, Category.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidDataException))]
    public void TestInvalidId()
    {
        Category.Id = 0;
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidDataException))]
    public void TestInvalidIdNegative()
    {
        Category.Id = -1;
    }

    [TestMethod]
    public void TestRelatedCategory()
    {
        Category categoryParent = new Category();
        categoryParent.Name = "Category Parent";
        
        Category.RelatedTo = categoryParent;

        Assert.AreEqual(Category.RelatedTo, categoryParent);
    }

    [TestMethod]
    public void TestNullRelatedCategory()
    {
        Category.RelatedTo = null;

        Assert.IsNull(Category.RelatedTo);
    }
}