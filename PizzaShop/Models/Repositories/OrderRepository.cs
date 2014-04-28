using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PizzaShop.Models.Catalog;
using System.Data.Entity;

namespace PizzaShop.Models.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private PizzaShopContext context;
        private DbSet<Order> dbSet;

        public OrderRepository(PizzaShopContext context)
        {
            this.context = context;
            this.dbSet = context.Set<Order>();
        }
        public IQueryable<Order> GetAll()
        {
            return dbSet;
        }

        public Order Get(int id)
        {
            return dbSet.FirstOrDefault(o => o.OrderId == id);
        }

        public void Add(Order entity)
        {
            dbSet.Add(entity);
        }

        public void Update(Order entity)
        {
            context.Entry(entity).State = System.Data.EntityState.Modified;
            foreach (var item in entity.Items)
            {
                context.Entry(item).State = System.Data.EntityState.Modified;
            }
        }

        public void Delete(Order entity)
        {
            dbSet.Remove(entity);
        }

        public void AddOrderStatus(OrderStatus status)
        {
            context.OrderStatuses.Add(status);
        }

        public void DeleteOrderStatus(OrderStatus status)
        {
            context.OrderStatuses.Remove(status);
        }

        public OrderStatus GetOrderStaus(int id)
        {
            return context.OrderStatuses.FirstOrDefault(os => os.OrderStatusId == id);
        }

        public IQueryable<OrderStatus> GetOrderStatuses()
        {
            return context.OrderStatuses;
        }
    }
}