using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PizzaShop.Models.Catalog
{
    public class OptionValue
    {
        #region Properties

        public int OptionValueId { get; set; }

        [Required(ErrorMessage="Name is required")]
        public string Name { get; set; }

        public string Image { get; set; }

        public int OptionId { get; set; }
        public virtual Option Option { get; set; }

        #endregion
    }
}