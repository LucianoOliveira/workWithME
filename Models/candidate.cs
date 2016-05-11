using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace workWithME.Models
{
    public class candidate
    {
        [Key]
        public int candidateId { get; set; }
        public string cdName { get; set; }
        public DateTime? cdDOB { get; set; }
        public string LinkedIn { get; set; }
        public int projectId { get; set; }
    }
}