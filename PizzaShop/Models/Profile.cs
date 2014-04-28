using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Models
{
    public class Profile
    {
        [Key]
        public int ProfileId { get; set; }

        public string Owner { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(30, ErrorMessage = "Name may not be longer than 30 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(30, ErrorMessage = "Name may not be longer than 30 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        public bool News { get; set; }
    }
}