using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PizzaShop.Models.Catalog
{   
    public class Option
    {
        #region Properties

        public int OptionId { get; set; }

        [Required(ErrorMessage="Name is required")]
        public string Name { get; set; }

        public virtual List<OptionValue> Values { get; set; }

        #endregion

        #region Constructor

        public Option()
        {
            Values = new List<OptionValue>();
        }

        #endregion

    }
}
