using Application.DTOs;
using Application.Interfaces;
using Core.Interfaces;
using Core.ValueObjects;
using System.Linq;

namespace Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<AvailableRoomDTO?> CheckAvailabilty(AvailabilityCriterionDTO availabilityCriteriondto)
        {
            
                // Mapear el DTO al Value Object
                var criterion = new AvailabilityCriterion(
                    availabilityCriteriondto.EntryDate,
                    availabilityCriteriondto.DepartureDate,
                    availabilityCriteriondto.RoomTypeId
                );

                var room = await _roomRepository.CheckAvailability(criterion);

                if (room == null)
                    return null;

                return new AvailableRoomDTO
                {
                    RoomNumber = room.RoomNumber,
                    RoomId = room.RoomId,
                    RoomTypeId = room.RoomTypeId,
                    RoomTypeName = room.RoomTypeName,
                    Description = room.Description,
                    Price = room.Price,
                    ImgUrl = room.ImgUrl,
                    CheckIn = room.CheckIn,
                    CheckOut = room.CheckOut,
                    ResultType = room.ResultType
                };         

            
        }


        public async Task UpdateRoomStatus(int id)
        {
            await _roomRepository.UpdateRoomStatus(id);

        }
        public async Task<List<StatusRoomDTO?>> StatusRoom()
        {
            var rooms = await _roomRepository.GetAllHotelStatusRooms();

            return rooms.Select(room => new StatusRoomDTO
            {
                RoomNumber = room.RoomNumber,
                RoomTypeName = room.RoomTypeName,
                Status = room.Status
            }).ToList();

        }

        public async Task<List<ManageActiveRoomDTO>> GetManageActiveRoomsAsync()
        {
            var rooms = await _roomRepository.GetManageActiveRoomsAsync();

            var result = rooms.Select(room => new ManageActiveRoomDTO
            {
                RoomTypeId = room.RoomTypeId,
                RoomTypeName = room.RoomTypeName,
                roomActiveDTOs = room.roomActiveDTOs.Select(r => new RoomActiveDTO
                {
                    RoomId = r.RoomId,
                    IsActice = r.IsActice,
                    RoomNumber = r.RoomNumber
                }).ToList()
            }).ToList();

            return result;
        }

        public async Task<bool> UpdateRoomActive(RoomActiveDTO Room)
        {

            var room = new RoomActive
            {
                RoomId = Room.RoomId,
                IsActice = Room.IsActice
            };

            return await _roomRepository.UpdateRoomActive(room);
        }

        public async Task<List<RoomsAvailableDTO>> ListAvailableRooms(AvailabilityCriterionDTO availabilityCriterion)
        {

            var criterion = new AvailabilityCriterion(
                availabilityCriterion.EntryDate,
                availabilityCriterion.DepartureDate,
                availabilityCriterion.RoomTypeId
            );

     
            var rooms = await _roomRepository.ListAvailableRooms(criterion);

            if (rooms == null || !rooms.Any())
                return new List<RoomsAvailableDTO>(); 

            // Mapear cada habitación al DTO
            return rooms.Select(room => new RoomsAvailableDTO
            {
                RoomNumber = room.RoomNumber,
                RoomType = room.RoomTypeName,
                Price = room.Price

            }).ToList();
        }
    }
 }
