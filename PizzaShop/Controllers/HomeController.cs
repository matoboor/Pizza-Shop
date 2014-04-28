using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaShop.Models;
using PizzaShop.Models.Catalog;

namespace PizzaShop.Controllers
{
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {
            ViewBag.Message = "Martin Boor";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Martin Boor";
            return View();
        }
    }
}
