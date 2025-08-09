using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ValueObjects
{
    public class ManageActiveRoom
    {
        public int RoomTypeId { get; set; }
         
        public string RoomTypeName { get; set; }

        public IEnumerable<RoomActive> roomActiveDTOs { get; set; } = new List<RoomActive>();

    }
}
