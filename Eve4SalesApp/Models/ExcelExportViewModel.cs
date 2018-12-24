using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eve4SalesApp.Models
{
    public class ExcelExportViewModel
    {


        public int Year { get; set; }
        public string Market { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Shipment { get; set; }

        public int DoneFlag { get; set; }

        public int NotAvailableFlag { get; set; }
        public int Tier { get; set; }

        public int Rank { get; set; }

        public DateTime? LastSourced { get; set; }

        public string Release { get; set; }

        public int PortalStatus { get; set; }

        public int ManualAdd { get; set; }

        public int SalesFiguresCalculatedFromRank { get; set; }

        [Key]
        public int MarketModelId { get; set; }

        

    }
}