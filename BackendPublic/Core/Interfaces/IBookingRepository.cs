using Core.Entities;
using Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBookingRepository
    {
        Task<BookingResponse> CreateBooking(Booking booking);
        Task<IEnumerable<InformationBooking>> AllBooking(int page);
        Task<bool> DeleteBooking(int idBooking);
    }
}
