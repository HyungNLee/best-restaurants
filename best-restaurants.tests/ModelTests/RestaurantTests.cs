using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using BestRestaurants.Models;

namespace BestRestaurants.Tests
{
  [TestClass]
  public class RestaurantTests : IDisposable
  {
    public RestaurantTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=best_restaurants_tests;";
    }

    public void Dispose()
    {
      Restaurant.DeleteAll();
      Cuisine.DeleteAll();
    }

    [TestMethod]
    public void Restaurant_DBstartsEmpty_Empty()
    {
      int result = Restaurant.GetAll().Count;

      Assert.AreEqual(0,result);
    }

    [TestMethod]
    public void Restaurant_HasSameValues_True()
    {
      //Arrange
      Restaurant restaurantOne = new Restaurant("One", "Seattle", 3, 1);
      Restaurant restaurantTwo = new Restaurant("One", "Seattle", 3, 2);

      //Assert
      Assert.AreEqual(restaurantOne, restaurantTwo);
    }

    [TestMethod]
    public void Edit_EditedItemHasDifferentValue_True()
    {
      //Arrange
      Restaurant restaurantOne = new Restaurant("One", "Seattle", 3);
      restaurantOne.Save();
      int id = restaurantOne.GetId();

      //Act
      restaurantOne.Edit("Two", "Portland", 4);
      Restaurant editedRestaurant = Restaurant.Find(id);

      //Assert
      Assert.AreEqual(restaurantOne, editedRestaurant);
    }

    [TestMethod]
    public void Delete_ItemIsDeletedFromDatabase_True()
    {
      //Arrange
      Restaurant restaurantOne = new Restaurant("One", "Seattle", 3);
      Restaurant defaultValues = new Restaurant ("", "", 0, 0);

      //Act
      restaurantOne.Save();
      int id = restaurantOne.GetId();
      restaurantOne.Delete(id);
      Restaurant notFound = Restaurant.Find(id);

      //Assert
      Assert.AreEqual(notFound, defaultValues);
    }
  }
}
