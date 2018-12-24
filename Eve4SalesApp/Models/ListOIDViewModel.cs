using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eve4SalesApp.Models
{
    public class ListOIDViewModel
    {

        [Key]
        public int SalesModelID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string BacklogGrp { get; set; }
        public int? GrpOID { get; set; }

    }
}