using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaShop.Areas.Admin.Models
{
    public class Route
    {
        public int RouteId { get; set; }
        public int RouteStatusId { get; set; }
        public int Duration { get; set; }
        public int Distance { get; set; }
        public string Owner { get; set; }
        public DateTime DateAdded { get; set; }
        public int Car { get; set; }
        public double CarConsumption { get; set; }
        public double PriceForLiter { get; set; }

        public double Costs
        {
            get
            {
                double _total = (Distance/1000)*(CarConsumption/100);
                return _total;
            }
        }

        public double TotalCosts
        {
            get
            {
                double _total = Costs * PriceForLiter;
                return _total;
            }
        }

        public double TotalProfit
        {
            get
            {
                double _total = OrdersProfit - TotalCosts;
                return _total;
            }
        }

        public double OrdersCosts { get; set; }
        public double OrdersTotal { get; set; }
        public double OrdersProfit
        {
            get
            {
                double _total = OrdersTotal - OrdersCosts;
                return _total;
            }
        }

        public virtual RouteStatus Status { get; set; }
        public virtual List<RoutePoint> RoutePoints { get; set; }
    }
}