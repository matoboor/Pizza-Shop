using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PizzaShop.Areas.Admin.Models;
using System.Data.Entity;

namespace PizzaShop.Models.Repositories
{
    public class RouteRepository : IRepository<Route>
    {
        private PizzaShopContext context;
        private DbSet<Route> dbSet;

        public RouteRepository(PizzaShopContext context)
        {
            this.context = context;
            this.dbSet = context.Set<Route>();
        }

        public IQueryable<Route> GetAll()
        {
            return dbSet;
        }

        public Route Get(int id)
        {
            return dbSet.FirstOrDefault(r => r.RouteId == id);
        }

        public void Add(Route entity)
        {
            dbSet.Add(entity);
        }

        public void Update(Route entity)
        {

            context.Entry(entity).State = System.Data.EntityState.Modified;
            foreach (var item in entity.RoutePoints)
            {
                context.Entry(item).State = System.Data.EntityState.Modified;
            }
        }

        public void Delete(Route entity)
        {
            dbSet.Remove(entity);
        }

        public IQueryable<RouteStatus> GetRouteStatuses()
        {
            return context.RouteStauses;
        }

        public void AddRouteStatus(RouteStatus status)
        {
            context.RouteStauses.Add(status);
        }

        //TODO Statuses
        public RouteStatus GetRouteStatus(int id)
        {
            return context.RouteStauses.FirstOrDefault(rs => rs.RouteStatusId == id);
        }

        public void DeleteRouteStatus(RouteStatus routeStatus)
        {
            context.RouteStauses.Remove(routeStatus);
        }

        //CARS

        public IQueryable<Car> GetCars()
        {
            return context.Cars;
        }

        public Car GetCar(int id)
        {
            return context.Cars.FirstOrDefault(c => c.CarId == id);
        }

        public void AddCar(Car car)
        {
            context.Cars.Add(car);
        }

        public void DeleteCar(Car car)
        {
            context.Cars.Remove(car);
        }
            
    }
}