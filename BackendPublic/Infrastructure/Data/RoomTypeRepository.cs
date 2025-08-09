using Core.Entities;
using Core.Interfaces;
using Core.ValueObjects;
using Dapper;
using Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private readonly AppDbContext _context;

        public RoomTypeRepository(AppDbContext context)
        {
            _context = context;
        }
        //connect the database
        private SqlConnection CreateConnection()
        {
            var connectionString = _context.Database.GetConnectionString();
            return new SqlConnection(connectionString);

        }
        public async Task<List<RoomType>> GetAllRoomType()
        {
            using SqlConnection connection = CreateConnection();
            await connection.OpenAsync();
            
            var roomTypes = await connection.QueryAsync<RoomType>(
                "sp_get_all_RoomType",
                commandType: System.Data.CommandType.StoredProcedure
            );
            return roomTypes.ToList();
        }

        public async Task<List<RoomType>> GetAllRoomTypeBySeason()
        {
            using SqlConnection connection = CreateConnection();
            await connection.OpenAsync();

            var roomTypes = await connection.QueryAsync<RoomType>(
                "sp_get_RoomType_season",
                commandType: System.Data.CommandType.StoredProcedure
            );
            return roomTypes.ToList();
        }

        public async Task<Response> UpdateRoomTypeData(RoomType roomType)
        {
            using SqlConnection connection = CreateConnection();
            await connection.OpenAsync();
            using SqlCommand command = new SqlCommand("sp_update_RoomType", connection);
            command.CommandType = CommandType.StoredProcedure;

            // Agregar parámetros al comando
            command.Parameters.Add(new SqlParameter("@RoomTypeId", SqlDbType.Int) { Value = roomType.RoomTypeId });
            command.Parameters.Add(new SqlParameter("@Price", SqlDbType.Int) { Value = roomType.Price });
            command.Parameters.Add(new SqlParameter("@Characteristics", SqlDbType.NVarChar, -1) { Value = roomType.Characteristics ?? (object)DBNull.Value });
            command.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, -1) { Value = roomType.description ?? (object)DBNull.Value });
            command.Parameters.Add(new SqlParameter("@Img", SqlDbType.NVarChar, -1) { Value = roomType.Image ?? (object)DBNull.Value });

            try
            {
                using SqlDataReader reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new Response
                    {
                        Code = reader.GetInt32("Code"), 
                        Message = reader.GetString("Message")
                    };
                }
            }
            catch (SqlException ex)
            {
                return new Response
                {
                    Code = -1,
                    Message = $"Error de base de datos: {ex.Message}"
                };
            }

            return new Response
            {
                Code = -1,
                Message = "No se pudo procesar la actualización"
            };
        }
        public async Task<RoomType?> GetRoomTypeByName(string roomTypeName)
        {
            using SqlConnection connection = CreateConnection();
            await connection.OpenAsync();

            var roomType = await connection.QueryFirstOrDefaultAsync<RoomType>(
                "sp_get_all_RoomType_Info",
                new { RoomTypeName = roomTypeName },
                commandType: System.Data.CommandType.StoredProcedure
            );
            return (RoomType?)roomType;
        }
    }
}
