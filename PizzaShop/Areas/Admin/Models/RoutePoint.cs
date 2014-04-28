using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaShop.Areas.Admin.Models
{
    public class RoutePoint
    {
        public int RoutePointId { get; set; }
        public int RouteId { get; set; }
        public int OrderId { get; set; }
        public string Address { get; set; }
        public int Duration { get; set; }
        public int Distance { get; set; }
    }
}
