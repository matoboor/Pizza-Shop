using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaShop.Models;
using PizzaShop.Models.Catalog;
using System.Web.Helpers;
using System.IO;
using PizzaShop.Models.Repositories;

namespace PizzaShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Employee")]
    public class OptionsController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /Admin/Options/
        public ActionResult Index()
        {
            var options = unitOfWork.optionRepository.GetAll().ToList();
            return View(options);
        }

        //
        // GET: /Admin/Options/Detail/4
        public ActionResult Detail(int id)
        {
            Option option = unitOfWork.optionRepository.Get(id);
            if (option == null)
            {
                return View("NotFound");
            }
            return View(option);
        }

        //
        // GET: /Admin/Options/Edit/4
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            Option option = unitOfWork.optionRepository.Get(id);
            if (option == null)
            {
                return View("NotFound");
            }
            return View(option);
        }

        //
        // POST: /Admin/Options/Edit/4
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(Option option, IEnumerable<HttpPostedFileBase> file)
        {
            if (ModelState.IsValid)
            {
                int i = 0;
                if (file != null)
                {
                    foreach (var _file in file)
                    {
                        if (_file != null && _file.ContentLength > 0)
                        {
                            if (!option.Values[i].Image.Equals("~/Uploads/Default.jpg"))
                            {
                                System.IO.File.Delete(Server.MapPath(option.Values[i].Image));
                            }
                            string image = "~/Uploads/option-" + option.Values[i].Name + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(_file.FileName);
                            string filePath = Server.MapPath(image);
                            _file.SaveAs(filePath);
                            option.Values[i].Image = image;
                        }
                        i++;
                    }
                }
                unitOfWork.optionRepository.Update(option);
                unitOfWork.Save();
                return RedirectToAction("Detail", new { id = option.OptionId });
            }
            return View(option);
        }

        //
        // GET: Admin/Options/New
        [Authorize(Roles = "Administrator")]
        public ActionResult New()
        {
            Option option = new Option();
            return View(option);
        }

        //
        // POST: Admin/Options/New/4
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult New(FormCollection formValues)
        {
            Option option = new Option();
            if (TryUpdateModel(option))
            {
                unitOfWork.optionRepository.Add(option);
                unitOfWork.Save();
                return RedirectToAction("Edit", new { id = option.OptionId });
            }
            return View(option);
        }

        //
        // GET: Admin/Options/Delete
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            Option option = unitOfWork.optionRepository.Get(id);
            if (option == null)
            {
                return View("NotFound");
            }
            return View(option);
        }

        //
        // POST: Admin/Options/Delete/4

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id, string confirmButton)
        {
            Option option = unitOfWork.optionRepository.Get(id);
            foreach (OptionValue ov in option.Values)
            {
                if (!ov.Image.Equals("~/Uploads/Default.jpg"))
                {
                    System.IO.File.Delete(Server.MapPath(ov.Image));
                }
            }
            var productOptions = unitOfWork.productRepository.GetProductOptions().Where(o => o.OptionId == option.OptionId);
            foreach (var po in productOptions)
            {
                unitOfWork.productRepository.DeleteProductOption(po);
            }
            unitOfWork.Save();
            unitOfWork.optionRepository.Delete(option);
            unitOfWork.Save();
            return View("Deleted");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult AddValue(int id)
        {
            OptionValue ov = new OptionValue();
            ov.OptionId = id;
            return View(ov);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddValue(int id, FormCollection formValues, HttpPostedFileBase file)
        {
            

            OptionValue ov = new OptionValue();
            if (TryUpdateModel(ov))
            {
                //TODO
                if (file != null && file.ContentLength > 0)
                {
                    string image = "~/Uploads/option" + ov.Name + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(file.FileName);
                    string filePath = Server.MapPath(image);
                    file.SaveAs(filePath);
                    ov.Image = image;
                }
                else
                {
                    ov.Image = "~/Uploads/Default.jpg";
                }
                
                unitOfWork.optionRepository.Get(id).Values.Add(ov);
                unitOfWork.Save();

                return RedirectToAction("Edit", new { id = ov.OptionId });
            }
            return View(ov);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteOptionValue(int id)
        {
            OptionValue ov = unitOfWork.optionRepository.GetOptionValue(id);
            if (!ov.Image.Equals("~/Uploads/Default.jpg"))
            {
                System.IO.File.Delete(Server.MapPath(ov.Image));
            }

            //Delete All Product Option Values associated with deleted option
            var productOptionValues = unitOfWork.productRepository.GetProductOptionValues().Where(pov => pov.OptionValueId == id);
            foreach (var pov in productOptionValues)
            {
                unitOfWork.productRepository.DeleteProductOptionValue(pov);
            }
            unitOfWork.Save();
            unitOfWork.optionRepository.DeleteOptionValue(ov);
            unitOfWork.Save();
            return RedirectToAction("Edit", new { id = ov.OptionId });
        }



    }
}
