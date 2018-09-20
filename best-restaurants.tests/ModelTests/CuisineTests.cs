using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using BestRestaurants.Models;

namespace BestRestaurants.Tests
{
  [TestClass]
  public class CuisineTests : IDisposable
  {
    public CuisineTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=best_restaurants_tests;";
    }

    public void Dispose()
    {
      Restaurant.DeleteAll();
      Cuisine.DeleteAll();
    }

    [TestMethod]
    public void Cuisine_DBstartsEmpty_Empty()
    {
      int result = Cuisine.GetAll().Count;

      Assert.AreEqual(0,result);
    }

    [TestMethod]
    public void Cuisine_HasSameValues_True()
    {
      Cuisine cuisineOne = new Cuisine("One");
      Cuisine cuisineTwo = new Cuisine("One");

      Assert.AreEqual(cuisineOne, cuisineTwo);
    }

    [TestMethod]
    public void Cuisine_FindMatchingCuisine_True()
    {
      Cuisine cuisineOne = new Cuisine("One");
      cuisineOne.Save();
      int id = cuisineOne.GetId();

      Cuisine foundCuisine = Cuisine.Find(id);

      Assert.AreEqual(cuisineOne, foundCuisine);
    }

    [TestMethod]
    public void Cuisine_CuisineDeletedFromDB_True()
    {
      //Arrange
      Cuisine cuisineOne = new Cuisine("One");
      cuisineOne.Save();
      int id = cuisineOne.GetId();
      Cuisine defaultCuisine = new Cuisine("", 0);

      //Act
      cuisineOne.Delete(id);
      Cuisine notFound = Cuisine.Find(id);

      //Assert
      Assert.AreEqual(notFound, defaultCuisine);
    }
    [TestMethod]
    public void Cuisine_EditedCuisineHasDifferentValue_True()
    {
      //Arrange
      Cuisine cuisineOne = new Cuisine ("One");
      Cuisine cuisineTwo = new Cuisine ("Two");
      cuisineOne.Save();
      int id = cuisineOne.GetId();

      //Act
      cuisineOne.Edit("Two");
      Cuisine foundCuisine = Cuisine.Find(id);

      //Assert
      Assert.AreEqual(cuisineTwo, foundCuisine);
    }

  }
}
