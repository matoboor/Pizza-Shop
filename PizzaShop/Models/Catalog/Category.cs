using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Models.Catalog
{
    public class Category
    {
        #region Properties
        public int CategoryId { get; set; }

        [Required(ErrorMessage="Name is required.")]
        [StringLength(30, ErrorMessage="Name may not be longer than 30 characters")]
        public string Name { get; set; }

        public int CategoryImageId { get; set; }
        public virtual CategoryImage Image { get; set; }
        #endregion
    }
}