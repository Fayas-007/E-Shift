using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_shift.Models
{
    public class Lorry
    {
        public int LorryID { get; set; }
        public string RegistrationNumber { get; set; }
        public string Model { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
