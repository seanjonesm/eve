using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eve4SalesApp.Models
{
    public class MapViewModel
    {
        [Key]
        public int MarketModelId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Market { get; set; }

        public int TotalSales { get; set; }
        public int TransID { get; set; }
        public int ForYear { get; set; }
        public bool DoneFlag { get; set; }
        public bool NotAvailableFlag { get; set; }
        public string MakeColour { get; set; }

        public int Tier { get; set; }

        public int ModelRank { get; set; }

        public string BacklogGrp { get; set; }
        public int? GrpOID { get; set; }
        public string ReleaseInfo { get; set; }

        public bool? ManualAdd { get; set; }

        public bool? SalesCalculatedFromRank { get; set; }

    }
}