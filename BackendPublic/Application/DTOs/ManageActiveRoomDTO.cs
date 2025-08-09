using Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ManageActiveRoomDTO
    {
        public int RoomTypeId { get; set; }

        public string RoomTypeName { get; set; }

        public IEnumerable<RoomActiveDTO> roomActiveDTOs { get; set; } = new List<RoomActiveDTO>();

    }
}
