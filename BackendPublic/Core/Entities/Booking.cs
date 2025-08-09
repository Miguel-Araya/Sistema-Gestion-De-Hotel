using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingID { get; set; }
        public int RoomID { get; set; }
        public virtual Room Room { get; set; }
        public DateTime CreationDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string BookingReferenceNumber { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Transaction { get; set; }
        public bool IsActive { get; set; }

        public Booking(Customer customer, int roomId, DateTime checkInDate,DateTime checkOutDate, int transaction)
        {
            Customer = customer;
            RoomID = roomId;
            CheckIn = checkInDate;
            CheckOut = checkOutDate;
            Transaction = transaction;
        }

        public Booking()
        {

        }

    }
}
