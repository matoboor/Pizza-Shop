using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaShop.Models.Catalog
{
    public class CartItem
    {
        public Product Product { get; set; }
        public List<CartItemOptionValue> Options { get; set; }
        public string Comment { get; set; }
        public double Total
        {
            get
            {
                double _total = Product.Price;
                foreach (var value in this.Options)
                {
                    _total += value.Value.Price;
                }
                return _total;
            }
        }

    }
}