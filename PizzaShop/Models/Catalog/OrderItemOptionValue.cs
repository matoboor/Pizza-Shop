using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaShop.Models.Catalog
{
    public class OrderItemOptionValue
    {
        public int OrderItemOptionValueId { get; set; }

        public int OrderItemId { get; set; }

        public int ArchivedProductOptionId { get; set; }

        public int ArchivedProductOptionValueId { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public double Price { get; set; }

        public virtual OrderItem OrderItem { get; set; }

    }
}