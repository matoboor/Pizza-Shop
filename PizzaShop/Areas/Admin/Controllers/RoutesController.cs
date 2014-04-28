using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaShop.Models.Repositories;
using PizzaShop.Models.Catalog;
using PizzaShop.Areas.Admin.Models;

namespace PizzaShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Employee")]
    public class RoutesController : Controller
    {

        private UnitOfWork unitOfWork = new UnitOfWork(); 

        // !!!get ready orders!!!
        // GET: /Admin/Routes/
        public ActionResult Index()
        {
            var readyOrders = unitOfWork.orderRepository.GetAll().OrderByDescending(o => o.DateAdded).Where(o => o.OrderStatus.Name.Equals("Ready"));
            var routes = unitOfWork.routeRepository.GetAll().Count();
            var cars = unitOfWork.routeRepository.GetCars();
            ViewData["cars"] = new SelectList(cars, "CarId", "Name");
            ViewBag.routes = routes;
            return View(readyOrders);
        }

        //get all routes
        // GET /Admin/Routes/All
        public ActionResult All()
        {
            var routes = unitOfWork.routeRepository.GetAll().ToList();
            return View(routes);
        }

        //Ajax
        // POST /Admin/Routes/Save + JSON route as data
        [HttpPost]
        [Authorize(Roles = "Administrator, Deliverer")]
        public ActionResult Save(Route route)
        {
            route.DateAdded = DateTime.Now;
            var routeStatus = unitOfWork.routeRepository.GetRouteStatuses().FirstOrDefault(rs => rs.Name.Equals("Pending To Delivery"));
            route.RouteStatusId = routeStatus.RouteStatusId;
            route.Owner = User.Identity.Name;
            route.CarConsumption = unitOfWork.routeRepository.GetCar(route.Car).Consumption;
            route.PriceForLiter = unitOfWork.routeRepository.GetCar(route.Car).PriceForLiter;
            var tmp_costs = 0.0;
            var tmp_total = 0.0;
            foreach (var point in route.RoutePoints)
            {
                var orderStatus = unitOfWork.orderRepository.GetOrderStatuses().FirstOrDefault(os => os.Name.Equals("Wait to delivery"));
                if (point.OrderId != 0)
                {
                    unitOfWork.orderRepository.Get(point.OrderId).OrderStatusId = orderStatus.OrderStatusId;
                    tmp_costs +=unitOfWork.orderRepository.Get(point.OrderId).Costs;
                    tmp_total += unitOfWork.orderRepository.Get(point.OrderId).Total;
                }
            }
            route.OrdersCosts = tmp_costs;
            route.OrdersTotal = tmp_total;
            unitOfWork.routeRepository.Add(route);
            unitOfWork.Save();
            return Json(route.RouteId);
        }

        //
        // GET /Admin/Routes/Details/5
        public ActionResult Details(int id)
        {
            var route = unitOfWork.routeRepository.Get(id);
            if (route == null)
            {
                return View("NotFound");
            }
            return View(route);
        }

        // GET /Admin/Routes/Delete/5
        [Authorize(Roles = "Administrator, Deliverer")]
        public ActionResult Delete(int id)
        {
            var route = unitOfWork.routeRepository.Get(id);
            if (route == null)
            {
                return View("NotFound");
            }
            foreach (var point in route.RoutePoints)
            {
                if (point.OrderId != 0)
                {
                    var order = unitOfWork.orderRepository.Get(point.OrderId);
                    if (order != null)
                    {
                        var status = unitOfWork.orderRepository.GetOrderStatuses().FirstOrDefault(os => os.Name.Equals("Ready"));
                        order.OrderStatusId = status.OrderStatusId;
                    }
                }
            }
            unitOfWork.routeRepository.Delete(route);
            unitOfWork.Save();
            return RedirectToAction("All");
        }

        [Authorize(Roles = "Administrator, Deliverer")]
        public ActionResult Edit(int id)
        {
            var route = unitOfWork.routeRepository.Get(id);
            if (route == null)
            {
                return View("NotFound");
            }
            var routeStatuses = unitOfWork.routeRepository.GetRouteStatuses();
            ViewBag.Statuses = new SelectList(routeStatuses,"RouteStatusId","Name");
            return View(route);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Deliverer")]
        public ActionResult Edit(int id, FormCollection formValues)
        {
            var route = unitOfWork.routeRepository.Get(id);
            if (TryUpdateModel(route))
            {
                unitOfWork.routeRepository.Update(route);
                if (route.Status.Name.Equals("Complete"))
                {
                    var status = unitOfWork.orderRepository.GetOrderStatuses().FirstOrDefault(os => os.Name.Equals("Complete"));
                    foreach (var order in route.RoutePoints)
                    {
                        if (order.OrderId != 0 && unitOfWork.orderRepository.Get(order.OrderId) != null)
                        {
                            unitOfWork.orderRepository.Get(order.OrderId).OrderStatusId = status.OrderStatusId;
                        }
                    }
                    var car = unitOfWork.routeRepository.GetCar(route.Car);
                    if (car != null)
                    {
                        car.KilometresDriven += route.Distance / 1000;
                    }
                }
                unitOfWork.Save();
                return RedirectToAction("All");
            }
            var routeStatuses = unitOfWork.routeRepository.GetRouteStatuses();
            ViewBag.Statuses = new SelectList(routeStatuses, "RouteStatusId", "Name");
            return View(route);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Statuses()
        {
            var statuses = unitOfWork.routeRepository.GetRouteStatuses();
            return View(statuses);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult AddStatus()
        {
            var status = new RouteStatus();
            return View(status);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddStatus(FormCollection formValues)
        {
            var status = new RouteStatus();
            if (TryUpdateModel(status))
            {
                if (unitOfWork.routeRepository.GetRouteStatuses().FirstOrDefault(os => os.Name == status.Name) != null)
                {
                    ModelState.AddModelError(string.Empty, "Status with the same name is in database yet. Change name.");
                    return View(status);
                }
                unitOfWork.routeRepository.AddRouteStatus(status);
                unitOfWork.Save();
                return RedirectToAction("Statuses");
            }
            return View(status);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult EditRouteStatus(int id)
        {
            var status = unitOfWork.routeRepository.GetRouteStatus(id);
            if (status == null)
            {
                return View("NotFound");
            }
            return View(status);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditRouteStatus(int id, FormCollection formValues)
        {
            var status = unitOfWork.routeRepository.GetRouteStatus(id);
            if (status.Permanent == true)
            {
                ModelState.AddModelError(string.Empty, "This is system status. Cannot be modified!");
            }
            if (TryUpdateModel(status))
            {
                unitOfWork.Save();
                return RedirectToAction("Statuses");
            }
            return View(status);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteRouteStatus(int id)
        {
            var status = unitOfWork.routeRepository.GetRouteStatus(id);
            if (status == null)
            {
                return View("NotFound");
            }
            return View(status);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteRouteStatus(int id, string confirmButoon)
        {
            var status = unitOfWork.routeRepository.GetRouteStatus(id);
            var items = unitOfWork.routeRepository.GetAll().Where(r => r.RouteStatusId == id).Count();
            if (status.Permanent == true)
            {
                ModelState.AddModelError(string.Empty, "This is system status. Cannot be modified!");
                return View(status);
            }
            else if (items > 0)
            {
                ModelState.AddModelError(string.Empty, "This status using "+items+" routes! You must change affected statuses first");
                return View(status);
            }
            unitOfWork.routeRepository.DeleteRouteStatus(status);
            unitOfWork.Save();
            return View("Deleted");
        }

        //CARS
        [Authorize(Roles = "Administrator")]
        public ActionResult Cars()
        {
            var cars = unitOfWork.routeRepository.GetCars();
            return View(cars);
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult AddCar()
        {
            Car car = new Car();
            return View(car);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult AddCar(FormCollection formValues)
        {
            Car car = new Car();
            if (TryUpdateModel(car))
            {
                if (unitOfWork.routeRepository.GetCars().FirstOrDefault(os => os.Name == car.Name) != null)
                {
                    ModelState.AddModelError(string.Empty, "Car with the same name is in database yet. Change name.");
                    return View(car);
                }
                unitOfWork.routeRepository.AddCar(car);
                unitOfWork.Save();
                return RedirectToAction("Cars");
            }
            return View(car);
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult EditCar(int id)
        {
            Car car = unitOfWork.routeRepository.GetCar(id);
            return View(car);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult EditCar(int id, FormCollection formValues)
        {
            Car car = unitOfWork.routeRepository.GetCar(id);
            if (TryUpdateModel(car))
            {
                unitOfWork.Save();
                return RedirectToAction("Cars");
            }
            return View(car);
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult CarDetail(int id)
        {
            return View(unitOfWork.routeRepository.GetCar(id));
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteCar(int id)
        {
            return View(unitOfWork.routeRepository.GetCar(id));
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult DeleteCar(int id, string confirmButton)
        {
            Car car = unitOfWork.routeRepository.GetCar(id);
            unitOfWork.routeRepository.DeleteCar(car);
            unitOfWork.Save();
            return RedirectToAction("Cars");
        }

        
   }
}
