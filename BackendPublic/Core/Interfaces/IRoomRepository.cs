using Core.Entities;
using Core.ValueObjects;

namespace Core.Interfaces
{
    public interface IRoomRepository
    {
        Task<AvailabilityRoom?> CheckAvailability(AvailabilityCriterion availabilityCriterion);
        Task<List<RoomsAvailable>> ListAvailableRooms(AvailabilityCriterion availabilityCriterion);

        Task UpdateRoomStatus(int roomId);

        Task<List<ManageActiveRoom>> GetManageActiveRoomsAsync();

        Task<List<StatusRoom>> GetAllHotelStatusRooms();

        Task<bool> UpdateRoomActive(RoomActive Room);

    }
}
