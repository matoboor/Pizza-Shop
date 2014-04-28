using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Areas.Admin.Models
{
    public class RouteStatus
    {
        public int RouteStatusId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(20, ErrorMessage = "Name may not be longer than 15 characters")]
        public string Name { get; set; }
        public bool? Permanent { get; set; }
    }
}
