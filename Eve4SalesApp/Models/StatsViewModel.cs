using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eve4SalesApp.Models
{
    public class StatsViewModel
    {

        [Key]
        public string Make { get; set; }
        public double gCalcValue { get; set; }
        public double yCalcValue { get; set; }
        public int ModelCount { get; set; }
        public int SalesYear { get; set; }
        public int SalesCount { get; set; }
        public double MarketShare { get; set; }
        public string MarketCode { get; set; }

    }
}