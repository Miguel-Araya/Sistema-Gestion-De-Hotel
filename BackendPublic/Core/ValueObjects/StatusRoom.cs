using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ValueObjects
{
    public class StatusRoom
    {
        public int RoomNumber { get; set; }
        public string RoomTypeName { get; set; }
        public string Status { get; set; }
    }
}
