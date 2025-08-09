using System.Data;
using Core.Entities;
using Core.Interfaces;
using Dapper;
using Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SeasonRepository : ISeasonRepository
    {
        private readonly AppDbContext _context;

        public SeasonRepository(AppDbContext context)
        {
            _context = context;
        }

        private SqlConnection CreateConnection()
        {
            var connectionString = _context.Database.GetConnectionString();
            return new SqlConnection(connectionString);
        }

        public async Task<List<Season>> GetAllSeasons()
        {
            using SqlConnection connection = CreateConnection();
            var result = await connection.QueryAsync<Season>(
                "GetAllCompleteSeasons",
                commandType: CommandType.StoredProcedure
            );
            return result.ToList();
        }

        public async Task<Season> GetSeasonById(int id)
        {
            using SqlConnection connection = CreateConnection();
            var result = await connection.QueryFirstOrDefaultAsync<Season>(
                "GetCompleteSeasonById",
                new { SeasonID = id },
                commandType: CommandType.StoredProcedure
            );
            return result;
        }

        public async Task<bool> CreateSeason(Season season)
        {
            using SqlConnection connection = CreateConnection();
            var result = await connection.ExecuteAsync(
                "CreateSeason",
                new
                {
                    season.SeasonName,
                    season.StartDate,
                    season.EndDate,
                    season.Percent,
                    season.IsActive,
                    season.IsHigh
                },
                commandType: CommandType.StoredProcedure
            );
            return result > 0;
        }

        public async Task<bool> UpdateSeason(Season season)
        {
            using SqlConnection connection = CreateConnection();
            var result = await connection.ExecuteAsync(
                "UpdateSeason",
                new
                {
                    season.SeasonID,
                    season.SeasonName,
                    season.StartDate,
                    season.EndDate,
                    season.Percent,
                    season.IsActive,
                    season.IsHigh
                },
                commandType: CommandType.StoredProcedure
            );
            return result > 0;
        }

        public async Task<bool> DeleteSeason(int id)
        {
            using SqlConnection connection = CreateConnection();
            var result = await connection.ExecuteAsync(
                "DeleteSeason",
                new { SeasonID = id },
                commandType: CommandType.StoredProcedure
            );
            return result > 0;
        }
    }
}
