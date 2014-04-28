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
    public class ProductsController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        
        //
        // GET: /Admin/Products/
        public ActionResult Index()
        {
            var products = unitOfWork.productRepository.GetAll().ToList();
            return View(products);
        }

        
        //
        // GET /Admin/Products/Detail/4
        public ActionResult Detail(int id)
        {
            Product product = unitOfWork.productRepository.Get(id);
            if (product == null)
            {
                return View("NotFound");
            }
            return View(product);
        }


        //
        // GET /Admin/Products/Edit/4
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            Product product = unitOfWork.productRepository.Get(id);
            if (product == null)
            {
                return View("NotFound");
            }
            var categories = unitOfWork.categoryRepository.GetAll();
            ViewData["categories"] = new SelectList(categories, "CategoryId", "Name");
            if (product == null)
                return View("NotFound");
            return View(product);
        }


        //
        // POST /Admin/Products/Edit4
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id, FormCollection formValues)
        {
            Product product = unitOfWork.productRepository.Get(id);
            if (product == null)
            {
                return View("NotFound");
            }
            if (TryUpdateModel(product))
            {
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(product);
        }


        //
        // GET /Admin/Products/New
        [Authorize(Roles = "Administrator")]
        public ActionResult New()
        {
            Product product = new Product();
            var categories = unitOfWork.categoryRepository.GetAll();
            ViewData["categories"] = new SelectList(categories,"CategoryId","Name");
            return View(product);
        }


        //
        // POST /Admin/Products/New
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult New(FormCollection formValues)
        {
            Product product = new Product();
            if (TryUpdateModel(product))
            {
                unitOfWork.productRepository.Add(product);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(product);
        }


        //
        // GET /Admin/Products/Delete/4
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            Product product = unitOfWork.productRepository.Get(id);
            if (product == null)
            {
                return View("NotFound");
            }
            return View(product);
        }


        //
        // POST /Admin/Products/Delete/4
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id, string confirmButton)
        {
            Product product = unitOfWork.productRepository.Get(id);
            //delete images
            foreach (var img in product.Images)
            {
                if (!img.Url.Equals("~/Uploads/Default.jpg"))
                {
                    System.IO.File.Delete(Server.MapPath(img.Url));
                }
            }
            unitOfWork.productRepository.Delete(product);
            unitOfWork.Save();
            return View("Deleted");
        }

        //
        // GET: /Admin/Products/AddProductOption/4
        [Authorize(Roles = "Administrator")]
        public ActionResult AddProductOption(int id)
        { 
            ProductOption po = new ProductOption();
            po.ProductId = id;
            var options = unitOfWork.optionRepository.GetAll();
            ViewBag.Options = new SelectList(options, "OptionId", "Name");
            return View(po);
        }

        //
        // POST: /Admin/Products/AddProductOption/4
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddProductOption(int id, FormCollection formValues)
        {
            ProductOption productOption = new ProductOption();
            if (TryUpdateModel(productOption))
            {
                unitOfWork.productRepository.Get(id).Options.Add(productOption);
                unitOfWork.Save();
                return RedirectToAction("Edit", new { id = productOption.ProductId });
            }
            var options = unitOfWork.optionRepository.GetAll();
            ViewBag.Options = new SelectList(options, "OptionId", "Name");
            return View(productOption);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteProductOption(int id)
        {
            ProductOption po = unitOfWork.productRepository.GetProductOption(id);
            if (po == null)
            {
                return View("NotFound");
            }
            unitOfWork.productRepository.DeleteProductOption(po);
            unitOfWork.Save();
            return RedirectToAction("Edit", new { id = po.ProductId });
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult EditProductOption(int id)
        {
            ProductOption productOption = unitOfWork.productRepository.GetProductOption(id);
            if (productOption == null)
            {
                return View("NotFound");
            }
            ViewBag.OptionValues = unitOfWork.optionRepository.Get(productOption.OptionId).Values;
            return View(productOption);
        }
        
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditProductOption(ProductOption productOption)
        {
            if (ModelState.IsValid)
            {

                unitOfWork.productRepository.UpdateProductOption(productOption);
                unitOfWork.Save();
                return RedirectToAction("Detail", new { id = productOption.ProductId });
            }
            ViewBag.OptionValues = unitOfWork.optionRepository.Get(productOption.OptionId).Values;
            return View(productOption);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult AddProductOptionValue(int id)
        {
            ProductOptionValue pov = new ProductOptionValue();
            pov.ProductOptionId = id;
            var optionValues = unitOfWork.optionRepository.Get(unitOfWork.productRepository.GetProductOption(id).OptionId).Values;
            ViewBag.OptionValues = new SelectList(optionValues, "OptionValueId", "Name");
            return View(pov);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddProductOptionValue(int id, FormCollection formValues)
        {
            ProductOptionValue pov = new ProductOptionValue();
            if (TryUpdateModel(pov))
            {
                unitOfWork.productRepository.GetProductOption(id).Values.Add(pov);
                unitOfWork.Save();
                return RedirectToAction("EditProductOption", new { id = pov.ProductOptionId });
            }
            var optionValues = unitOfWork.optionRepository.Get(id).Values;
            ViewBag.OptionValues = new SelectList(optionValues, "OptionValueId", "Name");
            return View(pov);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteProductOptionValue(int id)
        {
            ProductOptionValue productOptionValue = unitOfWork.productRepository.GetProductOptionValue(id);
            if (productOptionValue == null)
            {
                return View("NotFound");
            }
            unitOfWork.productRepository.DeleteProductOptionValue(productOptionValue);
            unitOfWork.Save();
            return RedirectToAction("EditProductOption", new { id = productOptionValue.ProductOptionId });
        }

        //Images
        [Authorize(Roles = "Administrator")]
        public ActionResult AddProductImage(int id)
        {
            ProductImage image = new ProductImage();
            image.ProductId=id;
            return View(image);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddProductImage(int id, FormCollection formValues, HttpPostedFileBase file)
        {
            ProductImage image = new ProductImage();
            if(TryUpdateModel(image) && (file != null && file.ContentLength > 0))
            {
                string url = "~/Uploads/product-" + image.ProductId + "-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(file.FileName);
                    string filePath = Server.MapPath(url);
                    file.SaveAs(filePath);
                    image.Url = url;
                    unitOfWork.productRepository.Get(id).Images.Add(image);
                    unitOfWork.Save();
                    return RedirectToAction("Edit", new { id = image.ProductId });
            }
            return View(image);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult EditProductImages(int id)
        {
            Product product = unitOfWork.productRepository.Get(id);
            if (product == null)
            {
                return View("NotFound");
            }
            return View(product);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditProductImages(Product product, IEnumerable<HttpPostedFileBase> file)
        {
            if (ModelState.IsValid)
            {
                int i = 0;
                foreach (var _file in file)
                {
                    if (_file != null && _file.ContentLength > 0)
                    {                
                        System.IO.File.Delete(Server.MapPath(product.Images[i].Url));
                        string url = "~/Uploads/product-" + product.ProductId + "-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(_file.FileName);
                        string filePath = Server.MapPath(url);
                        _file.SaveAs(filePath);
                        product.Images[i].Url = url;
                    }
                    i++;

                }
                unitOfWork.productRepository.UpdateProductImages(product.Images);
                unitOfWork.Save();
                return RedirectToAction("Detail", new { id = product.ProductId });
            }
            return View(product);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteProductImage(int id)
        {
            ProductImage productImage = unitOfWork.productRepository.GetProductImage(id);
            if (productImage == null)
            {
                return View("NotFound");
            }
            if(!productImage.Url.Equals("~/Uploads/Default.jpg"))
            {
                System.IO.File.Delete(Server.MapPath(productImage.Url));
            }
            unitOfWork.productRepository.DeleteProductImage(productImage);
            unitOfWork.Save();
            return RedirectToAction("EditProductImages", new { id = productImage.ProductId });
        }

    }
}
