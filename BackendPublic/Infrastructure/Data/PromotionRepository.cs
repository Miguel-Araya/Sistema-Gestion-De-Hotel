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
    public class PromotionRepository : IPromotionRepository
    {
        private readonly AppDbContext _context;

        public PromotionRepository(AppDbContext context)
        {
            _context = context;
        }

        //connect the database
        private SqlConnection CreateConnection()
        {
            var connectionString = _context.Database.GetConnectionString();
            return new SqlConnection(connectionString);

        }

        //With this method, we can get all the promotions that are always shown in the system
        public async Task<List<Promotion>> GetAllMainPromotion()
        {

            using SqlConnection connection = CreateConnection();
            await connection.OpenAsync();

            var promotions = await connection.QueryAsync<Promotion>(
                "GetMainPromotions",
                commandType: CommandType.StoredProcedure
            );

            return promotions.ToList();

        }
        
        public async Task<List<Promotion>> GetAllPromotions()
        {
            using var connection = CreateConnection();
            return (await connection.QueryAsync<Promotion>("GetAllPromotions", commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<Promotion?> GetPromotionById(int id)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Promotion>(
                "GetPromotionById",
                new { PromotionID = id },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> CreatePromotion(Promotion promotion)
        {
            using var connection = CreateConnection();

            return await connection.ExecuteScalarAsync<int>(
                "CreatePromotion",
                new
                {
                    promotion.PromotionName,
                    promotion.StartDate,
                    promotion.EndDate,
                    promotion.IsActive,
                    promotion.Percent,
                    promotion.Img
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> UpdatePromotion(Promotion promotion)
        {
            using var connection = CreateConnection();
            var rows = await connection.ExecuteAsync(
                "UpdatePromotion",
                promotion,
                commandType: CommandType.StoredProcedure);
            return rows > 0;
        }

        public async Task<bool> DeletePromotion(int id)
        {
            using var connection = CreateConnection();
            var rows = await connection.ExecuteAsync(
                "DeletePromotion",
                new { PromotionID = id },
                commandType: CommandType.StoredProcedure);
            return rows > 0;
        }
        
        
    }
}
