using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaShop.Models.Repositories;
using PizzaShop.Models;

namespace PizzaShop.Controllers
{
    [Authorize(Roles="Customer")]
    public class ProfilesController : BaseController
    {

        private UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /Profiles/

        public ActionResult Index()
        {
            var orders = db.orderRepository.GetAll().Where(o => o.User.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase));
            var profile = db.profileRepository.Get(User.Identity.Name);
            ViewBag.profile = profile;
            return View(orders);
        }

        public ActionResult OrderDetail(int id)
        {
            var order = db.orderRepository.Get(id);
            if (order != null)
            {
                if (order.User.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase))
                {
                    return View(order);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult CreateProfile()
        {
            Profile profile = new Profile();
            return View(profile);
        }

        [HttpPost]
        public ActionResult CreateProfile(FormCollection fromValues)
        {
            var profile = new Profile();
            if (TryUpdateModel(profile))
            {
                profile.Owner = User.Identity.Name;
                unitOfWork.profileRepository.Add(profile);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(profile);
        }

        public ActionResult EditProfile()
        {
            var profile = unitOfWork.profileRepository.Get(User.Identity.Name);

            return View(profile);
        }

        [HttpPost]
        public ActionResult EditProfile(FormCollection formValues)
        {
            var profile = unitOfWork.profileRepository.Get(User.Identity.Name);
            if (TryUpdateModel(profile))
            {
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(profile);
            }



    }
}
