using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaShop.Models.Catalog
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }

        public int ArchivedProductId { get; set; }
        public double Price { get; set; }
        public double Costs { get; set; }
        public string Name { get; set; }
        public string Wish { get; set; }
        public double Total { get; set; }

        public double Profit
        {
            get
            {
                double _total = Price - Costs;
                return _total;
            }
        }
        
        public virtual Order Order { get; set; }
        public virtual List<OrderItemOptionValue> Options { get; set; }

        public OrderItem()
        {
            this.Options = new List<OrderItemOptionValue>();
        }
    }
}