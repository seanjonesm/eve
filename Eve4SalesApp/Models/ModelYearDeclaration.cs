using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eve4SalesApp.Models
{

    [Table("CoverageMaps")]
    public class ModelYearDeclaration
    {

        [Key]
        public int TransID { get; set; }
        public int MarketModelId { get; set; }
        public int ForYear { get; set; }
        public bool DoneFlag { get; set; }
        public bool NotAvailableFlag { get; set; }
        public string BacklogInfo { get; set; }           
        public string ReleaseInfo { get; set; }

    }
}