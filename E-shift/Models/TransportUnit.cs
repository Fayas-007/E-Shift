using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_shift.Models
{
    public class TransportUnit
    {
        public int TransportUnitID { get; set; }
        public string UnitName { get; set; }
        public int? LorryID { get; set; }
        public int? DriverID { get; set; }
        public int? AssistantID { get; set; }
        public int? ContainerID { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsAvailable { get; set; }
    }
}
