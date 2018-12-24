using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eve4SalesApp.Models
{
    public class AggEuList
    {
        [Key]
        public int EURank { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int TimeFrame { get; set; }
        public string BacklogGrp { get; set; }
        public int TotalEU { get; set; }
        public string MarketList { get; set; }
        public bool Coverage { get; set; }
       

    }
}