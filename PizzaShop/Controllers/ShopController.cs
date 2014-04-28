using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaShop.Models.Catalog;
using PizzaShop.Models;
using System.Web.Security;

namespace PizzaShop.Controllers
{
    public class ShopController : BaseController
    {
        //
        // GET: /Shop/


        public ActionResult Products(string category)
        {
            if (!String.IsNullOrEmpty(category))
            {
                var products = db.productRepository.GetAll(category);
                ViewBag.CategoryName = category;
                return View(products);
            }
            else 
            {
                var products = db.productRepository.GetAll();
                if (products == null) { System.Diagnostics.Debug.WriteLine("ni4"); };
                ViewBag.CategoryName = "All";
                return View(products); 
            }
        }

        public ActionResult AddToCart(int id)
        {
            Product product = db.productRepository.Get(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult AddToCart(int id, FormCollection form)
        {
            Product product = db.productRepository.Get(id);
            string comment = form["Comment"];
            List<ProductOptionValue> options = new List<ProductOptionValue>();
            foreach (var option in product.Options)
            {
                options.Add(option.Values.FirstOrDefault(v => v.ProductOptionValueId == Convert.ToInt32(form[option.Name])));
            }
            Cart1.My().AddItem(product, options, comment);
            //Cart.Instance.AddItem(product, options, comment);
            return RedirectToAction("Products",new {category=product.Category.Name});
        }

        public ActionResult RemoveFromCart(int id)
        {
            Cart1.My().RemoveItem(id);

            //Cart.Instance.RemoveItem(id);
            return RedirectToAction("Products");
        }

        //
        // GET: /Shop/Checkout
        [Authorize(Roles="Customer,Vendor,Administrator")]
        public ActionResult Checkout()
        {
            Order order = new Order();
            Profile profile = db.profileRepository.Get(User.Identity.Name);
            if (profile != null)
            {
                order.Adress = profile.Address;
                order.FirstName = profile.FirstName;
                order.LastName = profile.LastName;
            }
            order.User = User.Identity.Name;
            order.Mail = Membership.GetUser().Email;
            
            return View(order);
        }

        //
        // POST /Shop/Checkout
        [HttpPost]
        [Authorize(Roles = "Customer,Vendor,Administrator")]
        public ActionResult Checkout(FormCollection formValues)
        {
            Order order = new Order();
            //Prevent against reload page and resend order to system
            if (/*PizzaShop.Models.Catalog.Cart.Instance.Items.Count() == 0*/Cart1.My().Items.Count()==0)
            {
                return RedirectToAction("Products");
            }
            if(TryUpdateModel(order)) 
            {
                Order NewOrder = Cart1.My().CreateOrder(order);
                Cart1.My().ClearCart();
                //PizzaShop.Models.Catalog.Cart.Instance.ClearCart();
                var status = db.orderRepository.GetOrderStatuses().FirstOrDefault(os => os.Name.Equals("Pending"));
                NewOrder.OrderStatusId = status.OrderStatusId;
                NewOrder.User = User.Identity.Name;
                //cost
                foreach (var item in NewOrder.Items)
                {
                    var product_cost = db.productRepository.Get(item.ArchivedProductId).Costs;
                    item.Costs = product_cost;
                }
                db.orderRepository.Add(NewOrder);
                db.Save();
                return View("Accepted",NewOrder.OrderId);
            }
                return View(order);
        }
    }
}
