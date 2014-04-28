using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PizzaShop.Models.Repositories
{
    public class ProfileRepository : IRepository<Profile>
    {
        private PizzaShopContext context;
        private DbSet<Profile> dbSet;

        public ProfileRepository(PizzaShopContext context)
        {
            this.context = context;
            this.dbSet = context.Set<Profile>();
        }

        public IQueryable<Profile> GetAll()
        {
            return dbSet;
        }

        public Profile Get(int id)
        {
            throw new NotImplementedException();
        }

        public  Profile Get(string owner)
        {
            return dbSet.FirstOrDefault(p => p.Owner.Equals(owner, StringComparison.OrdinalIgnoreCase));
        }

        public void Add(Profile entity)
        {
            dbSet.Add(entity);
        }

        public void Update(Profile entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Profile entity)
        {
            dbSet.Remove(entity);
        }
    }
}