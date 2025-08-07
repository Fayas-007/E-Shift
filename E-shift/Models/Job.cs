using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_shift.Models
{
    public class Job
    {
        public int JobID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
    }
}
