using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Areas.Admin.Models
{
    public class Car
    {
        public int CarId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }

        [Range(0.99, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public double Consumption { get; set; }

        [Range(0.99, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public double PriceForLiter { get; set; }

        public double KilometresDriven { get; set; }

        public double GetGasolineConsumed()
        {
            return KilometresDriven * (Consumption / 100);
        }
    }

}