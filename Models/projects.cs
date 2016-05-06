using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace workWithME.Models
{
    public class projects
    {
        [Key]
        public int projectId { get; set; }
        public string project { get; set; }
        public int priorityNum { get; set; }
        public string priority { get; set; }
        public int clientId { get; set; }
        public string client { get; set; }
        public int locationId { get; set; }
        public string location { get; set; }
        public string description { get; set; }
        public int numCandidates { get; set; }
        public int numVacancies { get; set; }
        public DateTime? dueDate { get; set; }
        public byte active { get; set; }
    }
}