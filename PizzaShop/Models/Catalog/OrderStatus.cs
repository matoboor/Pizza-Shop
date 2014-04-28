using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Models.Catalog
{
    public class OrderStatus
    {
        public int OrderStatusId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(20, ErrorMessage = "Name may not be longer than 15 characters")]
        public string Name { get; set; }
        public bool? Permanent { get; set; }
    }
}