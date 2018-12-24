using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Foolproof; 

namespace Eve4SalesApp.Models
{
    public class UserEditModel
    {
        [Key]
        public string Make { get; set; }
        public string Model { get; set; }

        public List<string> MarketCode { get; set; }

        public int SalesYearBegin { get; set; }

        [GreaterThanOrEqualTo("SalesYearBegin")]
        public int SalesYearEnd { get; set; }

        public int Sales { get; set; }

        public bool? UnknownSales { get; set; }

        public int? Rank { get; set; }


    }
}