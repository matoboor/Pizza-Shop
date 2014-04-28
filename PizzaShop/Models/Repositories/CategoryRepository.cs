using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PizzaShop.Models.Catalog;
using System.Data.Entity;

namespace PizzaShop.Models.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private PizzaShopContext context;
        private DbSet<Category> dbSet;

        public CategoryRepository(PizzaShopContext context)
        {
            this.context = context;
            this.dbSet = context.Set<Category>();
        }

        public IQueryable<Category> GetAll()
        {
            return dbSet;
        }

        public Category Get(int id)
        {
            return dbSet.Find(id);
        }

        public void Add(Category entity)
        {
            dbSet.Add(entity);
        }

        public void Update(Category entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Category entity)
        {
            dbSet.Remove(entity);
        }
    }
}