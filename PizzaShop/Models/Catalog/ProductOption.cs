using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Models.Catalog
{
    public class ProductOption
    {
        #region Properties

        public int ProductOptionId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(30, ErrorMessage = "Name may not be longer than 30 characters")]
        public string Name { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int OptionId { get; set; }
        public virtual Option Option { get; set; }

        public virtual List<ProductOptionValue> Values { get; set; }

        #endregion

        #region Constructors

        public ProductOption()
        {
            Values = new List<ProductOptionValue>();
        }
        #endregion
    }
}