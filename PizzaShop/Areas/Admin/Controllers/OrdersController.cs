using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaShop.Models.Catalog;
using PizzaShop.Models;
using PizzaShop.Models.Repositories;

namespace PizzaShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Employee")]
    public class OrdersController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();


        //
        // GET: /Admin/Orders/

        public ActionResult Index()
        {
            var orders = unitOfWork.orderRepository.GetAll().OrderByDescending(o => o.DateAdded).ToList();
            return View(orders);
        }

        //
        // GET: /Admin/Orders/Detail/5
        public ActionResult Detail(int id)
        {
            Order order = unitOfWork.orderRepository.Get(id);
            if (order == null)
            {
                return View("NotFound");
            }
            return View(order);
        }

        //
        // GET: /Admin/Orders/Edit/4
        [Authorize(Roles = "Administrator,Vendor")]
        public ActionResult Edit(int id)
        {
            Order order = unitOfWork.orderRepository.Get(id);
            if (order == null)
            {
                return View("NotFound");
            }
            var statuses = unitOfWork.orderRepository.GetOrderStatuses();
            ViewBag.Statuses = new SelectList(statuses,"OrderStatusId","Name");
            return View(order);
        }

        //
        // POST: /Admin/Orders/Edit/4
        [HttpPost]
        [Authorize(Roles = "Administrator,Vendor")]
        public ActionResult Edit(int id, FormCollection formValues)
        {
            Order order = unitOfWork.orderRepository.Get(id);
            if (TryUpdateModel(order))
            {
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            var statuses = unitOfWork.orderRepository.GetOrderStatuses();
            ViewBag.Statuses = new SelectList(statuses, "OrderStatusId", "Name");
            return View(order);
        }

        //
        // GET: /Admin/Orders/Delete/5
        [Authorize(Roles="Administrator, Vendor")]
        public ActionResult Delete(int id)
        {
            var order = unitOfWork.orderRepository.Get(id);
            if (order == null)
            {
                return View("NotFound");
            }
            return View(order);
        }

        //
        // POST: /Admin/Orders/Delete/5
        [HttpPost]
        [Authorize(Roles = "Administrator, Vendor")]
        public ActionResult Delete(int id, string submitButton)
        {
            var order = unitOfWork.orderRepository.Get(id);
            unitOfWork.orderRepository.Delete(order);
            unitOfWork.Save();
            return View("OrderDeleted");
        }

        //
        // GET: /Admin/Orders/Proceed/5
        [Authorize(Roles = "Administrator,Vendor,Deliverer")]
        public ActionResult Proceed(int id)
        {
            Order order = unitOfWork.orderRepository.Get(id);
            if (order == null)
            {
                return View("NotFound");
            }
            var statuses = unitOfWork.orderRepository.GetOrderStatuses();
            ViewBag.Statuses = new SelectList(statuses, "OrderStatusId", "Name");
            return View(order);
        }

        //
        // POST: /Admin/Orders/Proceed/5
        [HttpPost]
        [Authorize(Roles = "Administrator,Vendor,Deliverer")]
        public ActionResult Proceed(int id, FormCollection formValues)
        {
            Order order = unitOfWork.orderRepository.Get(id);
            if (TryUpdateModel(order))
            {
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            var statuses = unitOfWork.orderRepository.GetOrderStatuses();
            ViewBag.Statuses = new SelectList(statuses, "OrderStatusId", "Name");
            return View(order);
        }


        //
        // GET: /Admin/Orders/Status
        public ActionResult Status()
        {
            var statuses = unitOfWork.orderRepository.GetOrderStatuses();
            return View(statuses);
        }

        //
        // GET: /Admin/Orders/Status/AddOrderStatus
        [Authorize(Roles = "Administrator")]
        public ActionResult AddOrderStatus()
        {
            OrderStatus orderStatus = new OrderStatus();
            return View(orderStatus);
        }

        //
        // POST: /Admin/Orders/Status/AddOrderStatus
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddOrderStatus(FormCollection formValues)
        {
            OrderStatus orderStatus = new OrderStatus();
            if (TryUpdateModel(orderStatus))
            {
                if (unitOfWork.orderRepository.GetOrderStatuses().FirstOrDefault(os => os.Name == orderStatus.Name) != null)
                {
                    ModelState.AddModelError(string.Empty, "Status with the same name is in database yet. Change name.");
                    return View(orderStatus);
                }
                unitOfWork.orderRepository.AddOrderStatus(orderStatus);
                unitOfWork.Save();
                return RedirectToAction("Status");
            }
            return View(orderStatus);
        }

        //
        // GET: /Admin/Order/EditOrderStatus/5
        [Authorize(Roles = "Administrator")]
        public ActionResult EditOrderStatus(int id)
        {
            OrderStatus orderStatus = unitOfWork.orderRepository.GetOrderStaus(id);
            if (orderStatus == null)
            {
                return View("NotFound");
            }
            return View(orderStatus);
        }

        //
        // POST: /Admin/Order/EditOrderStatus/5
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditOrderStatus(int id, FormCollection formValues)
        {
            OrderStatus orderStatus = unitOfWork.orderRepository.GetOrderStaus(id);
            if (orderStatus.Permanent==true)
            {
                ModelState.AddModelError(string.Empty, "This is System Status! You cannot edit it.");
                return View(orderStatus);
            }
            if (TryUpdateModel(orderStatus))
            {
                unitOfWork.Save();
                return RedirectToAction("Status");
            }
            return View(orderStatus);
        }


        //
        // GET: /Admin/Order/OrderStatusDetail/5
        public ActionResult OrderStatusDetail(int id)
        {
            OrderStatus orderStatus = unitOfWork.orderRepository.GetOrderStaus(id);
            if (orderStatus == null)
            {
                return View("NotFound");
            }
            return View(orderStatus);
        }


        //
        // GET: /Admin/Order/DeleteOrderStatus/5
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteOrderStatus(int id)
        {
            OrderStatus orderStatus = unitOfWork.orderRepository.GetOrderStaus(id);
            if (orderStatus == null)
            {
                return View("NotFound");
            }
            return View(orderStatus);
        }

        //
        // POST: /Admin/Order/DeleteOrderStatus/5
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteOrderStatus(int id, string confirmButton)
        {
            OrderStatus orderStatus = unitOfWork.orderRepository.GetOrderStaus(id);
            if (orderStatus.Permanent == true)
            {
                ModelState.AddModelError(string.Empty, "This is System Status! You cannot delete it.");
                return View(orderStatus);
            } else
                if (unitOfWork.orderRepository.GetAll().Where(o => o.OrderStatusId == id).Count() > 0)
                {
                    ModelState.AddModelError(string.Empty, "Some orders are associated with this status. Change it before deleting");
                    return View(orderStatus);
                }
            unitOfWork.orderRepository.DeleteOrderStatus(orderStatus);
            unitOfWork.Save();
            return View("StatusDeleted");
        }


    }
}
