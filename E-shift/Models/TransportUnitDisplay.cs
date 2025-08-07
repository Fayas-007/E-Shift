using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_shift.Models
{
    internal class TransportUnitDisplay
    {
        public int TransportUnitID { get; set; }
        public string UnitName { get; set; }
        public string RegistrationNumber { get; set; }
        public string DriverName { get; set; }
        public string AssistantName { get; set; }
        public string ContainerNumber { get; set; }
        public string AvailableDisplay { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
