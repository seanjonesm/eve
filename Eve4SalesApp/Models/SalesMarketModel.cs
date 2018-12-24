using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eve4SalesApp.Models
{
    public class SalesMarketModel
    {
        [Key]
        public int MarketModelId { get; set; }
        public int SalesModelId { get; set; }
        public string MarketCode { get; set; }
        public DateTime LastSourced { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Backlog { get; set; }

    }
}