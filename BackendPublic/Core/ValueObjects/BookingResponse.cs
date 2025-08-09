using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ValueObjects
{
    public class BookingResponse
    {
        public string Status { get; set; }
        public string BookingReference { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }    
    }
}
