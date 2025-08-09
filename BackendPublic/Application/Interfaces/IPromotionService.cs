using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPromotionService
    {
       Task<List<PromotionMainDTO>> GetAllMainPromotionDto();
       Task<List<PromotionMainDTO>> GetAllPromotions();
       Task<PromotionMainDTO?> GetPromotionById(int id);
       Task<int> CreatePromotion(PromotionMainDTO dto);
       Task<bool> UpdatePromotion(PromotionMainDTO dto);
       Task<bool> DeletePromotion(int id);

    }
}
