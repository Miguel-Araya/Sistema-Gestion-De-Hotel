using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ValueObjects
{
    public class InformationBooking
    {
        public int bookingid { get; set; }
        public int RoomID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int CustomerID { get; set; }
        public int Transaction {  get; set; }
        public string BookingReferenceNumber { get; set; }
        public Customer Customer { get; set; } 
        public RoomType RoomType { get; set; }
    }
}
