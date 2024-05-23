﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using IServices;
using WebApi.Controllers;
using Moq;
using DTO.In;
using DTO.Out;
using Microsoft.AspNetCore.Mvc;

namespace TestWebApi;

[TestClass]
public class TestCategoryController
{
    private Mock<ICategoryService> _categoryServiceMock;

    [TestInitialize]
    public void SetUp()
    {
        _categoryServiceMock = new Mock<ICategoryService>(MockBehavior.Strict);
    }

    [TestMethod]
    public void TestCreateCategory()
    {

        var category = new Category
        {
            Id = 1,
            Name = "Test Category"
        };

        _categoryServiceMock.Setup(x => x.CreateCategory("Test Category")).Returns(category);

        var categoryController = new CategoryController(_categoryServiceMock.Object);
        var result = categoryController.CreateCategory(new CreateCategoryModel { Name = "Test Category" });
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.IsInstanceOfType(okResult.Value, typeof(CategoryModel));
        
        var categoryModel = okResult.Value as CategoryModel;
        Assert.IsNotNull(categoryModel);
        Assert.AreEqual(category.Id, categoryModel.Id);
    }

    [TestMethod]
    public void TestCreateCategoryWithNullInput()
    {
        var categoryController = new CategoryController(_categoryServiceMock.Object);

        var result = categoryController.CreateCategory(null);
        Assert.IsInstanceOfType(result, typeof(BadRequestResult));
    }

    [TestMethod]
    public void TestCreateCategoryWithEmptyName()
    {
        var categoryController = new CategoryController(_categoryServiceMock.Object);

        var result = categoryController.CreateCategory(new CreateCategoryModel());
        Assert.IsInstanceOfType(result, typeof(BadRequestResult));
    }

    [TestMethod]
    public void TestGetAllCategories()
    {
        var categories = new List<Category>()
        {
            new Category()
            {
                Id = 1,
                Name = "Electricista"
            },
            new Category()
            {
                Id = 2,
                Name = "Fontanero"
            },
            new Category()
            {
                Id = 3,
                Name = "Plomero"
            }
        };
        _categoryServiceMock.Setup(r => r.GetAll()).Returns(categories);
        var categoryController = new CategoryController(_categoryServiceMock.Object);
        var expectedCategories = new List<GetCategoryOutput>();
        categories.ForEach(category =>
        {
            expectedCategories.Add(new GetCategoryOutput(category));
        });

        var result = categoryController.Get(null);
        var okResult = result as OkObjectResult;
        var categoriesReturned = okResult.Value as List<GetCategoryOutput>;

        _categoryServiceMock.VerifyAll();
        CollectionAssert.AreEqual(categoriesReturned, expectedCategories);
    }

    [TestMethod]
    public void TestGetAllCategoriesEmpty()
    {
        var categories = new List<Category>();
        _categoryServiceMock.Setup(r => r.GetAll()).Returns(categories);
        var categoryController = new CategoryController(_categoryServiceMock.Object);

        var result = categoryController.Get(null);
        var okResult = result as OkObjectResult;
        var categoriesReturned = okResult.Value as List<GetCategoryOutput>;

        _categoryServiceMock.VerifyAll();
        Assert.AreEqual(categoriesReturned.Count, 0);
    }


    [TestMethod]
    public void TestGetCategory()
    {
        var category = new Category()
            {
                Id = 1,
                Name = "Electricista"
            };
        _categoryServiceMock.Setup(r => r.Get(It.IsAny<int>())).Returns(category);
        var categoryController = new CategoryController(_categoryServiceMock.Object);
        var expectedCategory = new GetCategoryOutput(category);

        var result = categoryController.Get(1);
        var okResult = result as OkObjectResult;
        var categoryReturned = okResult.Value as GetCategoryOutput;

        _categoryServiceMock.VerifyAll();
        Assert.AreEqual(categoryReturned, expectedCategory);
    }

    [TestMethod]
    public void TestGetCategoryNotFound()
    {
        Category category = null;
        _categoryServiceMock.Setup(r => r.Get(It.IsAny<int>())).Returns(category);
        var categoryController = new CategoryController(_categoryServiceMock.Object);

        var result = categoryController.Get(1);
        
        _categoryServiceMock.VerifyAll();
        Assert.IsTrue(result.GetType().Equals(typeof(NotFoundObjectResult)));
    }

}
