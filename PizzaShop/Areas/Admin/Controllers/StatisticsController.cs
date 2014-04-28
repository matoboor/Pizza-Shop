using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.IO;
using PizzaShop.Areas.Admin.Models;
using PizzaShop.Models;
using System.Collections;

namespace PizzaShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Employee")]
    public class StatisticsController : Controller
    {
        //
        // GET: /Admin/Statistics/
        private PizzaShopContext context = new PizzaShopContext();

        public ActionResult Index()
        {
            StatisticsViewModel ViewModel = new StatisticsViewModel();
            var products = context.Products;
            var statuses = context.OrderStatuses; 
            var values = new ChartValues();
            var values2 = new ChartValues();
            var values3 = new ChartValues();

            //charts
            foreach(var product in products)
            {
                var count = context.OrderItems.Where(o => o.Name == product.Name).Count();
                values.xValues.Add(product.Name);
                values.yValues.Add(count.ToString());
                System.Diagnostics.Debug.WriteLine(product.Name + " " + count);
            }

            foreach (var status in statuses)
            {
                var count = context.Orders.Where(o => o.OrderStatusId == status.OrderStatusId).Count();
                values2.xValues.Add(status.Name);
                values2.yValues.Add(count.ToString());
            }


            //orders
            ViewModel.Orders_total = context.Orders.Count();
            if (ViewModel.Orders_total > 0)
            {
                ViewModel.Orders_totalPrice = context.Orders.Sum(o => o.Total);
            }
            else ViewModel.Orders_totalPrice = 0;
            ViewModel.Orders_notComplete = context.Orders.Where(o => !o.OrderStatus.Name.Equals("Complete")).Count();
            ViewModel.Orders_complete = context.Orders.Where(o => o.OrderStatus.Name.Equals("Complete")).Count();
            
            ViewModel.Orders_total_thisMonth = context.Orders.Where(o => o.DateAdded.Month == DateTime.Now.Month).Count();
            if (ViewModel.Orders_total_thisMonth > 0)
                ViewModel.Orders_totalPrice_thisMonth = context.Orders.ToList().Where(o => o.DateAdded.Month == DateTime.Now.Month).Sum(o => o.Total);
            else
                ViewModel.Orders_totalPrice_thisMonth = 0;
            ViewModel.Orders_totalCosts = context.Orders.ToList().Sum(o => o.Costs);
            if (ViewModel.Orders_total_thisMonth > 0)
                ViewModel.Orders_totalCostsThisMonth = context.Orders.ToList().Where(o => o.DateAdded.Month == DateTime.Now.Month).Sum(o => o.Costs);
            else
                ViewModel.Orders_totalCostsThisMonth = 0;
            ViewModel.Orders_totalProfit = context.Orders.ToList().Sum(o => o.Profit);
            if (ViewModel.Orders_total_thisMonth > 0)
                ViewModel.Orders_totalProfitThisMonth = context.Orders.ToList().Where(o => o.DateAdded.Month == DateTime.Now.Month).Sum(o => o.Profit);
            else
                ViewModel.Orders_totalCostsThisMonth = 0;

            //Products
            ViewModel.Products_total = products.Count();
            ViewModel.Products_total_sold = context.OrderItems.Count();
            if (ViewModel.Orders_complete > 0)
                ViewModel.Products_total_sold_price = context.OrderItems.Where(oi => oi.Order.OrderStatus.Name.Equals("Complete")).Sum(oi => oi.Total);
            else ViewModel.Products_total_sold_price = 0;
            //Routes
            ViewModel.Routes_total = context.Routes.Count();
            ViewModel.Routes_planed = context.Routes.Where(r => r.Status.Name.Equals("Pending To Delivery")).Count();
            ViewModel.Routes_onTheWay = context.Routes.Where(r => r.Status.Name.Equals("On The Way")).Count();
            ViewModel.Routes_complete = context.Routes.Where(r => r.Status.Name.Equals("Complete")).Count();
            ViewModel.Routes_totalDistance = ViewModel.Routes_complete==0 ? 0 : context.Routes.Where(r => r.Status.Name.Equals("Complete")).Sum(r => r.Distance)/1000;
            ViewModel.Routes_totalTime = ViewModel.Routes_complete == 0 ? 0 : context.Routes.Where(r => r.Status.Name.Equals("Complete")).Sum(r => r.Duration)/3600;
            ViewModel.Routes_total_thisMonth = ViewModel.Routes_complete == 0 ? 0 : context.Routes.Where(r => r.DateAdded.Month == DateTime.Now.Month && r.Status.Name.Equals("Complete")).Count();
            ViewModel.Routes_total_distance_thisMonth = ViewModel.Routes_complete == 0 ? 0 : context.Routes.Where(r => r.Status.Name.Equals("Complete")).Sum(r => r.Distance) / 1000;
            ViewModel.Routes_total_time_thisMonth = ViewModel.Routes_complete == 0 ? 0 : context.Routes.Where(r => r.Status.Name.Equals("Complete")).Sum(r => r.Duration) / 3600;
            ViewModel.Routes_Costs = ViewModel.Routes_complete == 0 ? 0 : context.Routes.Where(r => r.Status.Name.Equals("Complete")).ToList().Sum(r => r.TotalCosts);
            ViewModel.Route_CostsThisMonth = ViewModel.Routes_complete == 0 ? 0 : context.Routes.Where(r => r.Status.Name.Equals("Complete") && r.DateAdded.Month == DateTime.Now.Month).ToList().Sum(r => r.TotalCosts);
            ViewModel.cars = context.Cars.ToList();
            ViewModel.values.Add("QuickProduct", values);
            ViewModel.values.Add("QuickOrder", values2);

            TempData["statistics"] = ViewModel.values;

            return View(ViewModel);
        }

        public ActionResult MyChart(string type, string val)
        {
            //TempData Will be erased when we reading from it
            System.Diagnostics.Debug.WriteLine(val);
            var data = TempData["statistics"] as IDictionary<string,ChartValues>;
            var values = data[val.ToString()];
            //So We need to fill up Temp data for next calling this function
            TempData["statistics"] = data;
            System.Diagnostics.Debug.WriteLine(values.xValues.Count);
            var bytes = new Chart(width: 400, height: 200)
                .AddSeries(
                    chartType: type,
                    xValue: values.xValues,
                    yValues: values.yValues)
                    .GetBytes("png");
            return File(bytes, "image/png");
        }


    }
}
