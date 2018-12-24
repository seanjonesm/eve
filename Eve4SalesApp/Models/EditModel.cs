using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eve4SalesApp.Models
{
    public class EditModel
    {

        public int CovTransId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }

        public string Market { get; set; }
        public int xMarket { get; set; }
        public int Year { get; set; }
        public int Sales { get; set; }
          public int Rank { get; set; }
        public bool DeleteModel { get; set; }

        public bool ManualAdd { get; set; }

        public bool RollOn { get; set; }

        public int RollOnYear { get; set; }
               
    }
}