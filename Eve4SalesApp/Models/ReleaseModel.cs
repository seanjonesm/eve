using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eve4SalesApp.Models
{

    [Table("CurrentRelease")]
    public class ReleaseModel 
    {

        [Key]
        public int TransID { get; set; }

        [Required]
        [RegularExpression("^R[0-9]-2[0-9]{3}$", ErrorMessage = "Release Number should take the form: RX-2XXX")]
        public string CurrentRelease { get; set; }

    }
}