using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaShop.Models;
using PizzaShop.Models.Catalog;
using PizzaShop.Models.Repositories;
using System.IO;

namespace PizzaShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Employee")]
    public class CategoriesController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        //
        // GET: /Admin/Categories/

        public ActionResult Index()
        {
            var categories = unitOfWork.categoryRepository.GetAll();
            return View(categories);
        }


        //
        // GET: /Admin/Categories/Detail/4
        public ActionResult Detail(int id)
        {
            Category category = unitOfWork.categoryRepository.Get(id);
            if (category == null)
                return View("NotFound");
            else
                return View(category);
        }


        //
        // GET /Admin/Categories/Edit/4
        [Authorize(Roles="Administrator")]
        public ActionResult Edit(int id)
        {
            Category category = unitOfWork.categoryRepository.Get(id);
            if (category == null)
                return View("NotFound");
            else
            return View(category);
        }


        //
        // POST /Admin/Categories/Edit/4
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id, FormCollection formValues, HttpPostedFileBase file)
        {
            //TODO Obraykz
            Category category = unitOfWork.categoryRepository.Get(id);
            if (TryUpdateModel(category))
            {
                if (file != null && file.ContentLength > 0)
                {
                    if (!category.Image.Url.Equals("~/Uploads/Default.jpg"))
                    {
                        System.IO.File.Delete(Server.MapPath(category.Image.Url));
                    }
                    string image = "~/Uploads/category-" + category.Name + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(file.FileName);
                    string filePath = Server.MapPath(image);
                    file.SaveAs(filePath);
                    category.Image.Url = image;
                }
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(category);
        }


        //
        // GET /Admin/Categories/New
        [Authorize(Roles = "Administrator")]
        public ActionResult New()
        {
            Category category = new Category();
            return View(category);
        }


        //
        // POST /Admin/Categories/New
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult New(FormCollection formValues, HttpPostedFileBase file)
        {
            Category category = new Category();
            category.Image = new CategoryImage();
            if (TryUpdateModel(category))
            {
                if (unitOfWork.categoryRepository.GetAll().Where(c => c.Name.Equals(category.Name)).Count() > 0)
                {
                    ModelState.AddModelError(string.Empty, "Category whith name: " + category.Name + " already exist. Change Name.");
                    return View(category);
                }
                if (file != null && file.ContentLength > 0)
                {
                    string image = "~/Uploads/category-" + category.Name + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(file.FileName);
                    string filePath = Server.MapPath(image);
                    file.SaveAs(filePath);
                    category.Image.Url = image;
                }
                else
                {
                    category.Image.Url = "~/Uploads/Default.jpg";
                }
                unitOfWork.categoryRepository.Add(category);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(category);
        }


        //
        //GET /Admin/Categories/Delete/4
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            Category category = unitOfWork.categoryRepository.Get(id);
            int products = unitOfWork.productRepository.GetAll().Where(p => p.CategoryId == id).Count();
            ViewBag.products = products;
            if (category == null)
                return View("NotFound");
            else
            return View(category);
        }


        //
        // POST /Admin/Categories/Delete/4
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id, string confirmButton)
        {
            Category category = unitOfWork.categoryRepository.Get(id);
            if (!category.Image.Url.Equals("~/Uploads/Default.jpg"))
            {
                System.IO.File.Delete(Server.MapPath(category.Image.Url));
            }
            if (category == null)
                return View("NotFound");

            unitOfWork.categoryRepository.Delete(category);
            unitOfWork.Save();

            return View("Deleted");
        }
    }
}
