using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaShop.Models.Repositories;
using System.Web.Security;
using PizzaShop.Areas.Admin.Models;

namespace PizzaShop.Areas.Admin.Controllers
{
    [Authorize(Roles="Employee")]
    public class UsersController : Controller
    {

        private UnitOfWork unitOfWork = new UnitOfWork();


        //
        // GET: /Admin/Users/
        public ActionResult Index()
        {
            UserViewModel model = new UserViewModel();
            var employers = Roles.GetUsersInRole("Employee");
            var customers = Roles.GetUsersInRole("Customer");
            foreach (var user in employers)
            {

                model.Employers.Add(Membership.GetUser(user));
             
            }
            foreach (var user in customers)
            {

                model.Customers.Add(Membership.GetUser(user));

            }

            return View(model);
        }

        //
        // GET: /Admin/Users/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            var roles = Roles.GetAllRoles().ToList();
            roles.Remove("Employee");
            roles.Remove("Customer");
            ViewData["Roles"] = new SelectList(roles);
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    Roles.AddUserToRole(model.UserName, "Employee");
                    Roles.AddUserToRole(model.UserName, model.Role);
                    return RedirectToAction("Index", "Users");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            var roles = Roles.GetAllRoles().ToList();
            roles.Remove("Employee");
            roles.Remove("Customer");
            ViewData["Roles"] = new SelectList(roles);
            return View(model);
        }

        //
        // GET: /Admin/Users/Detail/5
        public ActionResult Detail(string name)
        {
            MembershipUser user;
            if (string.IsNullOrEmpty(name))
            {
                user = Membership.GetUser(User.Identity.Name);
            }
            else { user = Membership.GetUser(name); };
            int orders = unitOfWork.orderRepository.GetAll().Where(o => o.User.Equals(name)).Count();
            int routes = unitOfWork.routeRepository.GetAll().Where(r => r.Owner.Equals(name)).Count();
            ViewBag.orders = orders;
            ViewBag.routes = routes;
            return View(user);
        }


        //
        // GET: /Admin/Users/Edit
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(string name)
        {
            var user = Membership.GetUser(name);
            var roles = Roles.GetAllRoles().ToList();
            roles.Remove("Employee");
            roles.Remove("Customer");
            
            var userRole = Roles.GetRolesForUser(name).ToList();
            userRole.Remove("Employee");
            ViewBag.roles = new SelectList(roles,userRole[0]);
            return View(user);
        }

        //
        // POST: /Admin/Users/Edit/Jano
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(string name, FormCollection formValues)
        {
            var user = Membership.GetUser(name);
            if (TryUpdateModel(user))
            {
                var userRole = Roles.GetRolesForUser(name).ToList();
                    userRole.Remove("Employee");
                Roles.RemoveUserFromRole(user.UserName, userRole[0]);
                Roles.AddUserToRole(user.UserName, formValues["Role"]);
                Membership.UpdateUser(user);
                return RedirectToAction("Index");
            }
            
            var roles = Roles.GetAllRoles().ToList();
            roles.Remove("Employee");
            roles.Remove("Customer");

            var _userRole = Roles.GetRolesForUser(name).ToList();
            if (_userRole.Contains("Employee"))
                _userRole.Remove("Employee");
            if (_userRole.Contains("Customer"))
                _userRole.Remove("Customer");
            
            ViewBag.roles = new SelectList(roles,_userRole[0]);
            return View(user);
        }


        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(PizzaShop.Models.ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return View("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        //
        //GET /Admin/Delete/fero
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(string name)
        {
            var user = Membership.GetUser(name);
            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(string name, string confirmButton)
        {
            var profile = unitOfWork.profileRepository.Get(name);
            if (profile != null)
            {
                unitOfWork.profileRepository.Delete(profile);
            }
            Membership.DeleteUser(name);
            return View("Deleted");
        }

        #region Status Codes
        public static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion


    }
}
