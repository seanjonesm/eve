using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eve4SalesApp.Models
{
    [Table("DataAccessByMake")]
    public class PortalStats
    {

        [Key]
        public string Make { get; set; }
        public string Colour { get; set; }
        public string Status { get; set; }
        public int StatusCode { get; set; }

    }
}