using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPromotionRepository
    {

        Task<List<Promotion>> GetAllMainPromotion();
        Task<List<Promotion>> GetAllPromotions();
        Task<Promotion?> GetPromotionById(int id);
        Task<int> CreatePromotion(Promotion promotion);
        Task<bool> UpdatePromotion(Promotion promotion);
        Task<bool> DeletePromotion(int id);
    }
}
