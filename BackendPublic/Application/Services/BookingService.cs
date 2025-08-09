using Application.DTOs;
using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;
using Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<IEnumerable<InformationBookingDTO>> AllBooking(int page)
        {
          IEnumerable<InformationBooking> bookings = await _bookingRepository.AllBooking(page);
            var bookingDTOs = bookings.Select(b => new InformationBookingDTO
            {
                bookingid = b.bookingid,
                RoomID= b.RoomID,
                CheckIn = b.CheckIn,
                CheckOut = b.CheckOut,
                CustomerID = b.CustomerID,
                Transaction = b.Transaction,
                BookingReferenceNumber = b.BookingReferenceNumber,
                Customer = new CustomerDTO
                {
                   
                    Name = b.Customer.CustomerName,
                    LastName = b.Customer.CustomerLastName,
                    Email = b.Customer.CustomerEmail,
                    CardNumber = b.Customer.CardNumber
                  
                },
                RoomType = new RoomTypeDTO
                {
                    RoomTypeName = b.RoomType.RoomTypeName,
                   
                 
                }
            });

            return bookingDTOs.ToList();
        }

        public async Task<BookingResponseDTO> CreateBooking(BookingDTO bookingDTO)
        {
            var customer=new Customer(
                bookingDTO.Customer.Name,
                bookingDTO.Customer.LastName,
                bookingDTO.Customer.Email,
                bookingDTO.Customer.CardNumber
            );

            var booking = new Booking(
                customer,
                bookingDTO.RoomId,
                bookingDTO.CheckIn,
                bookingDTO.CheckOut,
                bookingDTO.Transaction
            );
            var ResponseBooking = await _bookingRepository.CreateBooking(booking);
            return new BookingResponseDTO
            {
                Status = ResponseBooking.Status,
                BookingReference = ResponseBooking.BookingReference,
                Name = ResponseBooking.Name,
                LastName = ResponseBooking.LastName,
                Email = ResponseBooking.Email

            };
        }

        public async Task<bool> DeleteBooking(int idBooking)
        {
           return await _bookingRepository.DeleteBooking(idBooking);
        }
    }
}
