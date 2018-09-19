using Microsoft.AspNetCore.Mvc;
using BestRestaurants.Models;
using System;
using System.Collections.Generic;

namespace BestRestaurants.Controllers
{
    public class CuisinesController : Controller
    {
      [HttpGet("/cuisines")]
      public ActionResult Index()
      {
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View(allCuisines);
      }

      [HttpGet("/cuisines/new")]
      public ActionResult CreateForm()
      {
        return View();
      }

      [HttpPost("/cuisines")]
      public ActionResult Create()
      {
        Cuisine newCuisine = new Cuisine(Request.Form["cuisine-name"]);
        newCuisine.Save();
        return RedirectToAction("Index");
      }

      [HttpGet("/cuisines/{id}")]
      public ActionResult Detail(int id)
      {
        Cuisine selectedCuisine = Cuisine.Find(id);
        return View(selectedCuisine);
      }
    }
}
