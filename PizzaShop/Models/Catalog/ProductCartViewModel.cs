using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaShop.Models.Catalog
{
    public class ProductCartViewModel
    {
        public Product product { get; set; }
        public string Comment { get; set; }

        public ProductCartViewModel(Product product, string comment)
        {
            this.product = product;
            this.Comment = comment;
        }
    }
}