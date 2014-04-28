using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaShop.Models.Repositories
{
    public interface IRepository<T> where T : new()
    {
        IQueryable<T> GetAll();
        T Get(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
