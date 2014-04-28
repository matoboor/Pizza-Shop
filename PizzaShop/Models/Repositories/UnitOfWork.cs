using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PizzaShop.Models.Catalog;

namespace PizzaShop.Models.Repositories
{
    public class UnitOfWork
    {
        private PizzaShopContext context = new PizzaShopContext();

        public IRepository<Category> categoryRepository { get; set; }
        public OptionRepository optionRepository { get; set; }
        public ProductRepository productRepository { get; set; }
        public OrderRepository orderRepository { get; set; }
        public RouteRepository routeRepository { get; set; }
        public ProfileRepository profileRepository { get; set; }

        public UnitOfWork()
        {
            categoryRepository = new CategoryRepository(this.context);
            optionRepository = new OptionRepository(this.context);
            productRepository = new ProductRepository(this.context);
            orderRepository = new OrderRepository(this.context);
            routeRepository = new RouteRepository(this.context);
            profileRepository = new ProfileRepository(this.context);

        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}