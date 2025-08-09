using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AvailabilityCriterionDTO
    {
        public DateTime EntryDate { get; set;  }
        public DateTime DepartureDate { get; set; }
        public int RoomTypeId { get; set;  }
    }
}
