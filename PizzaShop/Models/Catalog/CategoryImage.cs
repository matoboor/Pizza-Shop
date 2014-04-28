using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Models.Catalog
{
    public class CategoryImage
    {
        #region Properties
        public int CategoryImageId { get; set; }

        public string Url { get; set; }
        #endregion
    }
}