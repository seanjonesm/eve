using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eve4SalesApp.Models
{
    public class MarketOverviewViewModel
    {
        [Key]
        public int SalesYear { get; set; }

        public double naVal { get; set; }
        public double deVal { get; set; }
        public double frVal { get; set; }
        public double ukVal { get; set; }
        public double itVal { get; set; }

    }
}