using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PizzaShop.Models.Catalog;
using System.Data.Entity;

namespace PizzaShop.Models.Repositories
{
    public class OptionRepository : IRepository<Option>
    {
        private PizzaShopContext context;
        private DbSet<Option> dbSet;

        public OptionRepository(PizzaShopContext context)
        {
            this.context = context;
            this.dbSet = context.Set<Option>();
        }

        public IQueryable<Option> GetAll()
        {
            return dbSet;
        }

        public Option Get(int id)
        {
            return dbSet.Where(o => o.OptionId==id).Include(o => o.Values).SingleOrDefault();
        }

        public void Add(Option entity)
        {
            dbSet.Add(entity);
        }

        public void Update(Option entity)
        {
            context.Entry(entity).State = System.Data.EntityState.Modified;
            foreach (var val in entity.Values)
            {
                context.Entry(val).State = System.Data.EntityState.Modified;
            }
        }

        public void Delete(Option entity)
        {
            context.Options.Remove(entity);
        }

        //Extended method
        public OptionValue GetOptionValue(int id)
        {
            return context.OptionValues.FirstOrDefault(ov => ov.OptionValueId == id);
        }

        //Extended method
        public void DeleteOptionValue(OptionValue value)
        {
            this.context.OptionValues.Remove(value);
        }
    }
}