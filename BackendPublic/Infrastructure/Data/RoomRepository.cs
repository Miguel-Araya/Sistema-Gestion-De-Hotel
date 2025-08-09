using Core.Entities;
using Core.Interfaces;
using Core.ValueObjects;
using Dapper;
using Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace Infrastructure.Data
{
    public class RoomRepository : IRoomRepository
    {
        private readonly AppDbContext _context;

        public RoomRepository(AppDbContext context)
        {
            _context = context;
        }

        //connect the database
        private SqlConnection CreateConnection()
        {
            var connectionString = _context.Database.GetConnectionString();
            return new SqlConnection(connectionString);

        }


        public async Task<AvailabilityRoom?> CheckAvailability(AvailabilityCriterion availabilityCriterion)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new
                {
                    StartTime = availabilityCriterion.EntryDate,
                    EndTime = availabilityCriterion.DepartureDate,
                    RoomType = availabilityCriterion.RoomTypeId,
                };

                var query = "sp_check_availability";

                // Ejecutamos la consulta y obtenemos una lista de AvailabilityRoom(un value object)
                var rooms = await connection.QueryAsync<AvailabilityRoom>(
                    query,
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                );

                // Si la lista está vacía, retornamos null; sino, retornamos el primer elemento
                return rooms.FirstOrDefault();
            }
        }
        public async Task<List<RoomsAvailable>> ListAvailableRooms(AvailabilityCriterion availabilityCriterion)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new
                {
                    StartTime = availabilityCriterion.EntryDate,
                    EndTime = availabilityCriterion.DepartureDate,
                    RoomType = availabilityCriterion.RoomTypeId,
                };

                var query = "sp_list_available_rooms";

                // Ejecutamos la consulta y obtenemos una lista de RoomsAvailable(un value object)
                var rooms = await connection.QueryAsync<RoomsAvailable>(
                    query,
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                );

                // Si la lista está vacía, retornamos null; sino, retornamos el primer elemento
                return (List<RoomsAvailable>)rooms;
            }
        }

        public async Task UpdateRoomStatus(int roomId)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new { roomId = roomId };
                var query = "sp_enable_room";

                await connection.ExecuteAsync(
                    query,
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                );
            }
        }

        public async Task<List<StatusRoom>> GetAllHotelStatusRooms()
        {
            using SqlConnection connection = CreateConnection();

            await connection.OpenAsync();

            var rooms = await connection.QueryAsync<StatusRoom>(
                "sp_getHotelRoomStatusToday",
                commandType: System.Data.CommandType.StoredProcedure
            );

            return rooms.ToList();
        }

        public async Task<List<ManageActiveRoom>> GetManageActiveRoomsAsync()
        {
            using var connection = CreateConnection();

            var sql = "sp_getManageActiveRooms";

            var roomDictionary = new Dictionary<int, ManageActiveRoom>();

            var result = await connection.QueryAsync<ManageActiveRoom, RoomActive, ManageActiveRoom>(
                sql,
                (roomType, room) =>
                {
                    if (!roomDictionary.TryGetValue(roomType.RoomTypeId, out var manageRoom))
                    {
                        manageRoom = new ManageActiveRoom
                        {
                            RoomTypeId = roomType.RoomTypeId,
                            RoomTypeName = roomType.RoomTypeName,
                            roomActiveDTOs = new List<RoomActive>()
                        };
                        roomDictionary.Add(manageRoom.RoomTypeId, manageRoom);
                    }

                    ((List<RoomActive>)manageRoom.roomActiveDTOs).Add(room);
                    return manageRoom;
                },
                splitOn: "RoomId",
                commandType: System.Data.CommandType.StoredProcedure
            );

            return roomDictionary.Values.ToList();
        }

        public async Task<bool> UpdateRoomActive(RoomActive Room)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new
                {
                    RoomId = Room.RoomId,
                    IsActice = Room.IsActice
                };

                var result = await connection.QuerySingleAsync<int>(
                    "sp_UpdateRoomActive",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return result == 1;
            }
        }

      
    }


}
