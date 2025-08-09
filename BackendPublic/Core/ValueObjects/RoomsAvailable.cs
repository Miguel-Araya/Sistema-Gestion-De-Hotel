using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ValueObjects
{
    public class RoomsAvailable
    {
        public int RoomNumber { get; set; }
        public string RoomTypeName { get; set; }
        public int Price { get; set; }
    }
}
