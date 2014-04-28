using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaShop.Models.Catalog
{
    public class ProductOptionValue
    {
        #region Properites
        public int ProductOptionValueId { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public bool Default { get; set; }

        public int ProductOptionId { get; set; }
        public virtual ProductOption ProductOption { get; set; }

        public int OptionValueId { get; set; }
        public virtual OptionValue Value { get; set; }

        #endregion

    }
}
