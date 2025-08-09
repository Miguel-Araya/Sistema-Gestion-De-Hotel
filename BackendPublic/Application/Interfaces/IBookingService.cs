using Application.DTOs;
using Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBookingService
    {
        Task<BookingResponseDTO> CreateBooking(BookingDTO bookingDTO);
        Task<IEnumerable<InformationBookingDTO>> AllBooking(int page);
        Task<bool> DeleteBooking(int idBooking);
    }
}
