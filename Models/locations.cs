using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace workWithME.Models
{
    public class locations
    {
        [Key]
        public int locationID { get; set; }
        public string locationName { get; set; }
    }
}