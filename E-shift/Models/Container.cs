using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_shift.Models
{
    public class Container
    {
        public int ContainerID { get; set; }
        public string ContainerNumber { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedAt { get; set; }

        public string AvailableDisplay => IsAvailable ? "Yes" : "No";
    }
}
