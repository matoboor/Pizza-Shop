using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace PizzaShop.Areas.Admin.Models
{
    public class StatisticsViewModel
    {
        //Products
        public int Products_total { get; set; }
        public int Products_total_sold { get; set; }
        public double Products_total_sold_price { get; set; }

        //ORDERS

        public int Orders_total { get; set; }
        public int Orders_notComplete { get; set; }
        public int Orders_complete { get; set; }
        public double Orders_totalPrice { get; set; }
        public int Orders_total_thisMonth { get; set; }
        public double? Orders_totalPrice_thisMonth { get; set; }
        public double? Orders_totalCosts { get; set; }
        public double? Orders_totalCostsThisMonth { get; set; }
        public double? Orders_totalProfit { get; set; }
        public double? Orders_totalProfitThisMonth { get; set; }


        //Routes
        public int Routes_total { get; set; }
        public int Routes_complete { get; set; }
        public int Routes_planed { get; set; }
        public int Routes_onTheWay { get; set; }
        public double Routes_totalDistance { get; set; }
        public int Routes_totalTime { get; set; }
        public int Routes_total_thisMonth { get; set; }
        public double Routes_total_distance_thisMonth { get; set; }
        public int Routes_total_time_thisMonth { get; set; }
        public double Routes_Costs { get; set; }
        public double Route_CostsThisMonth { get; set; }

        //Cars
        public IList<Car> cars { get; set; }

        public IDictionary<string, ChartValues> values { get; set; }

        public StatisticsViewModel()
        {
            Routes_total = 0;
            Routes_complete = 0;
            Routes_planed = 0;
            Routes_onTheWay = 0;
            Routes_totalDistance = 0;
            Routes_totalTime = 0;
            Routes_total_thisMonth = 0;
            Routes_total_distance_thisMonth = 0;
            Routes_total_time_thisMonth = 0;

            Products_total = 0;
            Products_total_sold = 0;
            Products_total_sold_price = 0;

            Orders_total = 0;
            Orders_notComplete = 0;
            Orders_complete = 0;
            Orders_totalPrice = 0;
            Orders_total_thisMonth = 0;
            Orders_totalPrice_thisMonth = 0;

            values = new Dictionary<string, ChartValues>();
        }
    }
}