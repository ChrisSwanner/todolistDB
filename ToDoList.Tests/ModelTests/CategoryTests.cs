using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ToDoList.Models;
using System;

namespace ToDoList.Tests
{
  [TestClass]
  public class CategoryTests : IDisposable
  {
    public void Dispose()
    {
      Item.DeleteAll();
      Category.DeleteAll();
    }

    public CategoryTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=todo_test;";
    }

    [TestMethod]
    public void GetItems_RetrievesAllItemsWithCategoryId_ItemList()
    {
      Category testCategory = new Category("Household Chores", 1);
      testCategory.Save();

      Item firstItem = new Item("Mow the lawn", "2008-01-01", 1, testCategory.GetId());
      firstItem.Save();
      Item secondItem = new Item("Do the dishes", "2008-01-01", 2,  testCategory.GetId());
      secondItem.Save();

      List<Item> testItemList = new List<Item> {firstItem, secondItem};
      List<Item> resultItemList = testCategory.GetItems();

      CollectionAssert.AreEqual(testItemList, resultItemList);
    }

    [TestMethod]
    public void GetAll_CategoriesEmptyAtFirst_0()
    {
      //arrange, act
      int result = Category.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueForSameName_Category()
    {
      //Arrange, Act
      Category firstCategory = new Category("Household Chores");
      Category secondCategory = new Category("Household Chores");

      //assert
      Assert.AreEqual(firstCategory, secondCategory);
    }

    [TestMethod]
    public void Save_SavesCategoryToDatabase_CategoryList()
    {
      //arrange
      Category testCategory = new Category("Household Chores");
      testCategory.Save();

      //act
      List<Category> result = Category.GetAll();
      List<Category> testList = new List<Category>{testCategory};

      //assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToCategory_Id()
    {
      //arrange
      Category testCategory = new Category("Household Chores");
      testCategory.Save();

      //act
      Category savedCategory = Category.GetAll()[0];

      int result = savedCategory.GetId();
      int testId = testCategory.GetId();

      //assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_FindsCategoryInDatabase_Category()
    {
      //arrange
      Category testCategory = new Category("Household Chores");
      testCategory.Save();

      //act
      Category foundCategory = Category.Find(testCategory.GetId());

      //assert
      Assert.AreEqual(testCategory, foundCategory);
    }
  }
}
