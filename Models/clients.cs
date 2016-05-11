using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace workWithME.Models
{
    public class clients
    {
        [Key]
        public int clientID { get; set; }
        public string clientName { get; set; }
    }
}