using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Models.Catalog
{
    public class Product
    {
        #region Properties
        public int ProductId { get; set; }

        [Required(ErrorMessage="Name is required")]
        [StringLength(30,ErrorMessage="Name may not be longer than 30 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage="Price is required")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.09, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public double Costs { get; set; }

        public double Weight { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int DescriptionId { get; set; }
        public virtual Description Description { get; set; }

        public virtual List<ProductImage> Images { get; set; }

        public virtual List<ProductOption> Options { get; set; }
        #endregion

        #region Constructors
        public Product()
        {
            this.Images = new List<ProductImage>();
        }
        #endregion
    }

}
