using Core.Entities;
using Core.Interfaces;
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
    public class AdRepository : IAdRepository
    {

        private readonly AppDbContext _context;

        public AdRepository(AppDbContext context)
        {
            _context = context;
        }

        //connect the database
        private SqlConnection CreateConnection()
        {
            var connectionString = _context.Database.GetConnectionString();
            return new SqlConnection(connectionString);
        }

        //With this method, we can get all the ads that are always shown in the system
        public async Task<List<Ad>> GetAllMainAd()
        {
            using SqlConnection connection = CreateConnection();
            await connection.OpenAsync();

            var ads = await connection.QueryAsync<Ad>(
                "GetMainAds",
                commandType: CommandType.StoredProcedure
            );

            return ads.ToList();
        }

        public async Task<List<Ad>> GetAllAdminAd()
        {
            using SqlConnection connection = CreateConnection();
            await connection.OpenAsync();

            var ads = await connection.QueryAsync<Ad>(
                "sp_GetAdminAds",
                commandType: CommandType.StoredProcedure
            );

            return ads.ToList();
        }

        public async Task<Ad> GetAdById(int id)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Ad>(
                "sp_GetAdById",
                new { AdID = id },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> UpdateAd(Ad ad)
        {
            using var connection = CreateConnection();
            var rows = await connection.ExecuteAsync(
                "sp_UpdateAd",
                ad,
                commandType: CommandType.StoredProcedure);
            return rows > 0;
        }

        public async Task<bool> DeleteAd(int id)
        {
            using var connection = CreateConnection();
            var rows = await connection.ExecuteAsync(
                "sp_DeleteAd",
                new { AdID = id },
                commandType: CommandType.StoredProcedure);
            return rows > 0;
        }

        public async Task<int> CreateAd(Ad ad)
        {
            var parameters = new
            {
                Name = ad.Name,
                StartDate = ad.StartDate,
                EndDate = ad.EndDate,
                IsActive = ad.IsActive,
                Img = ad.Img,
                ImgUrl = ad.ImgUrl
            };

            using var connection = CreateConnection();
            return await connection.ExecuteScalarAsync<int>(
                "sp_CreateAd",
                parameters,
                commandType: CommandType.StoredProcedure);
        }
    }
}
