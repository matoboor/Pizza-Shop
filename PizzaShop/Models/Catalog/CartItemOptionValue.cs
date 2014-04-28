using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaShop.Models.Catalog
{
    public class CartItemOptionValue
    {
        public ProductOption Option { get; set; }
        public ProductOptionValue Value { get; set; }

    }
}