using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaShop.Models.Repositories;
using PizzaShop.Models.Catalog;

namespace PizzaShop.Controllers
{
    public class BaseController : Controller
    {
        protected UnitOfWork db = new UnitOfWork();
        protected DateTime time = DateTime.Now;
        protected ShoppingCart Cart1=new ShoppingCart();

                
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewBag.Categories = db.categoryRepository.GetAll().ToList();
            ViewBag.cart = Cart1.My();
            base.OnActionExecuted(filterContext);
        }

    }
}
