using Application.DTOs;
using Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRoomService
    {
        Task<AvailableRoomDTO?> CheckAvailabilty(AvailabilityCriterionDTO availabilityCriterion);

        Task UpdateRoomStatus(int id);

        Task<List<ManageActiveRoomDTO>> GetManageActiveRoomsAsync();

        Task<List<StatusRoomDTO?>> StatusRoom();

        Task<bool> UpdateRoomActive(RoomActiveDTO Room);
        Task<List<RoomsAvailableDTO>> ListAvailableRooms(AvailabilityCriterionDTO availabilityCriterion);


    }
}
