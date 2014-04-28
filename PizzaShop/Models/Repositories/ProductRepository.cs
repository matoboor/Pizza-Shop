using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PizzaShop.Models.Catalog;
using System.Data.Entity;

namespace PizzaShop.Models.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private PizzaShopContext context;
        private DbSet<Product> dbSet;

        public ProductRepository(PizzaShopContext context)
        {
            this.context = context;
            this.dbSet = context.Set<Product>();
        }


        public IQueryable<Product> GetAll()
        {
            return dbSet;
        }

        public Product Get(int id)
        {
            return dbSet.FirstOrDefault(p => p.ProductId == id);
        }

        public void Add(Product entity)
        {
            dbSet.Add(entity);
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product entity)
        {
            dbSet.Remove(entity);
        }

        //extended method
        public IQueryable<Product> GetAll(int CategoryID)
        {
            return from product in dbSet
                   where product.CategoryId == CategoryID
                   select product;
        }

        //extended method
        public IQueryable<Product> GetAll(string category)
        {
            return from product in dbSet
                   where product.Category.Name.Equals(category)
                   select product;
        }

        //extended method
        public IQueryable<ProductOption> GetProductOptions()
        {
            return context.ProductOptions;
        }

        //extended method
        public ProductOption GetProductOption(int ProductOptionId)
        {
            return context.ProductOptions.FirstOrDefault(po => po.ProductOptionId == ProductOptionId);
        }

        //extended method
        public void DeleteProductOption(ProductOption productOption)
        {
            context.ProductOptions.Remove(productOption);
        }

        //extended method
        public void UpdateProductOption(ProductOption productOption)
        {
            context.Entry(productOption).State = System.Data.EntityState.Modified;
            foreach (var pov in productOption.Values)
            {
                context.Entry(pov).State = System.Data.EntityState.Modified;
            }
        }

        //extended method
        public ProductOptionValue GetProductOptionValue(int id)
        {
            return context.ProductOptionValues.FirstOrDefault(pov => pov.ProductOptionValueId == id);
        }

        //extended method
        public IQueryable<ProductOptionValue> GetProductOptionValues()
        {
            return context.ProductOptionValues;
        }

        //extended method
        public void DeleteProductOptionValue(ProductOptionValue productOptionValue)
        {
            context.ProductOptionValues.Remove(productOptionValue);
        }

        public ProductImage GetProductImage(int id)
        {
            return context.ProductImages.FirstOrDefault(pi => pi.ProductImageId == id);
        }

        public void UpdateProductImages(IEnumerable<ProductImage> images)
        {
            foreach (var img in images)
            {
                context.Entry(img).State = System.Data.EntityState.Modified;
            }
        }

        public void DeleteProductImage(ProductImage productImage)
        {
            context.ProductImages.Remove(productImage);
        }
    }

}