using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class InformationBookingDTO
    {
        public int bookingid { get; set; }
        public int RoomID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int CustomerID { get; set; }
        public int Transaction { get; set; }
        public string BookingReferenceNumber { get; set; }
        public CustomerDTO Customer { get; set; }
        public RoomTypeDTO RoomType { get; set; }
    }
}
