using Microsoft.AspNetCore.Mvc;
using BestRestaurants.Models;
using System;
using System.Collections.Generic;

namespace BestRestaurants.Controllers
{
    public class RestaurantsController : Controller
    {
      [HttpGet("/restaurants")]
      public ActionResult Index()
      {
        List<Restaurant> allRestaurants = Restaurant.GetAll();
        return View(allRestaurants);
      }

      [HttpGet("/restaurants/new")]
      public ActionResult CreateForm()
      {
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View(allCuisines);
      }

      [HttpPost("/restaurants")]
      public ActionResult Create()
      {
        Restaurant newRestaurant = new Restaurant(Request.Form["restaurant-name"], Request.Form["restaurant-city"], int.Parse(Request.Form["cuisine"]));
        newRestaurant.Save();
        return RedirectToAction("Index");
      }

      [HttpGet("/restaurants/{id}")]
      public ActionResult Detail(int id)
      {
        Restaurant newRestaurant = Restaurant.Find(id);
        return View(newRestaurant);
      }

      [HttpGet("/restaurants/{id}/update")]
      public ActionResult UpdateForm(int id)
      {
        Dictionary<string, object> newDictionary = new Dictionary<string, object>() {};
        List<Cuisine> allCuisines = Cuisine.GetAll();
        Restaurant foundRestaurant = Restaurant.Find(id);
        newDictionary.Add("cuisine", allCuisines);
        newDictionary.Add("restaurant", foundRestaurant);
        return View(newDictionary);
      }

      [HttpPost("/restaurants/{id}/update")]
      public ActionResult Update(int id)
      {
        Restaurant thisRestaurant = Restaurant.Find(id);
        thisRestaurant.Edit(Request.Form["new-name"], Request.Form["new-city"], int.Parse(Request.Form["cuisine"]));
        return RedirectToAction("Index");
      }

      [HttpGet("/restaurants/delete")]
      public ActionResult DeleteAll()
      {
        Restaurant.DeleteAll();
        return RedirectToAction("Index");
      }

      [HttpGet("/restaurants/{id}/delete")]
      public ActionResult Delete(int id)
      {
        Restaurant thisRestaurant = Restaurant.Find(id);
        thisRestaurant.Delete(id);
        return RedirectToAction("Index");
      }
    }
}
