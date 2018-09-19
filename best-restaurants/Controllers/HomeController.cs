using Microsoft.AspNetCore.Mvc;
using BestRestaurants.Models;
using System;
using System.Collections.Generic;

namespace BestRestaurants.Controllers
{
    public class HomeController : Controller
    {
      [HttpGet("/")]
      public ActionResult Index()
      {
        return View();
      }
    }
}
