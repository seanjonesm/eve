using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eve4SalesApp.Models
{
    public class SalesFigure
    {
        [Key]
        public int TransID { get; set; }
        public int MarketModelId { get; set; }
        public int TimeFrame { get; set; }
        public int TotalSales { get; set; }
        public int Tier { get; set; }
        public int ModelRank { get; set; }
        public bool? ManualAdd { get; set; }
        public bool? SalesCalculatedFromRank { get; set; }
    }
}