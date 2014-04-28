using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace PizzaShop.Areas.Admin.Models
{
    public class ChartValues
    {
        public IList xValues { get; set; }
        public IList yValues { get; set; }

        public ChartValues()
        {
            xValues = new List<string>();
            yValues = new List<string>();
        }
    }

    
    
}