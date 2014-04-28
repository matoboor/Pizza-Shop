using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PizzaShop.Models.Catalog;
using PizzaShop.Areas.Admin.Models;

namespace PizzaShop.Models
{
    public class PizzaShopContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryImage> CategoryImages { get; set; }
        public DbSet<Description> Descriptions { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }
        public DbSet<ProductOptionValue> ProductOptionValues { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<OptionValue> OptionValues { get; set; }
        //Order
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<OrderItemOptionValue> OrderItemOtionValues { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        //Route
        public DbSet<RouteStatus> RouteStauses { get; set; }
        public DbSet<RoutePoint> RoutePoints { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Car> Cars { get; set; }
        //profile
        public DbSet<Profile> Profiles { get; set; }

    }
}