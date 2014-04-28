using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Models.Catalog
{
    public class Order
    {
        #region Properties

        public int OrderId { get; set; }
        
        public int OrderStatusId { get; set; }
        
        public string User { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(30, ErrorMessage = "Name may not be longer than 30 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(30, ErrorMessage = "Name may not be longer than 30 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Email is is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Adress is required")]
        public string Adress { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(15, ErrorMessage = "Phone number may not be longer than 15 characters")]
        public string Phone { get; set; }

        public string Status { get; set; }

        public double Total { get; set; }

        public double Costs
        {
            get
            {
                double _total = 0;
                foreach (var item in Items)
                {
                    _total += item.Costs;
                }
                return _total;
            }
        }

        public double Profit
        {
            get
            {
                double _total = 0;
                foreach (var item in Items)
                {
                    _total += item.Profit;
                }
                return _total;
            }
        }
        public DateTime DateAdded { get; set; }

        public virtual OrderStatus OrderStatus { get; set; }
        public virtual List<OrderItem> Items { get; set; }

        #endregion

        #region Constructors
        public Order()
        {
            this.Items = new List<OrderItem>();
        }
        #endregion

    }
}