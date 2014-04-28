using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Models.Catalog
{
    public class ProductImage
    {
        #region Properties
        public int ProductImageId { get; set; }

        public string Url { get; set; }

        [StringLength(30, ErrorMessage="Title may not be longer than 30 characters")]
        public string Title { get; set; } 

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        #endregion
    }
}