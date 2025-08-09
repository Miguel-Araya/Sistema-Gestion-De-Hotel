using Application.DTOs;
using Application.Interfaces;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Application.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository _promotionRepository;

        public PromotionService(IPromotionRepository promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        public async Task<List<PromotionMainDTO>> GetAllMainPromotionDto()
        {
            
            var promotions = await _promotionRepository.GetAllMainPromotion();

            return promotions.Select(promotion => new PromotionMainDTO
            {
                PromotionID = promotion.PromotionID,
                PromotionName = promotion.PromotionName,
                Img = promotion.Img,
            }).ToList();

        }
        
        public async Task<List<PromotionMainDTO>> GetAllPromotions()
        {
            var promotions = await _promotionRepository.GetAllPromotions();
            return promotions.Select(p => new PromotionMainDTO()
            {
                PromotionID = p.PromotionID,
                PromotionName = p.PromotionName,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                IsActive = p.IsActive,
                Percent = p.Percent,
                Img = p.Img
            }).ToList();
        }

        public async Task<PromotionMainDTO?> GetPromotionById(int id)
        {
            var p = await _promotionRepository.GetPromotionById(id);
            if (p == null) return null;
            return new PromotionMainDTO
            {
                PromotionID = p.PromotionID,
                PromotionName = p.PromotionName,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                IsActive = p.IsActive,
                Percent = p.Percent,
                Img = p.Img
            };
        }

        public async Task<int> CreatePromotion(PromotionMainDTO dto)
        {
            var p = new Promotion
            {
                PromotionName = dto.PromotionName,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsActive = dto.IsActive,
                Percent = dto.Percent,
                Img = dto.Img
            };
            return await _promotionRepository.CreatePromotion(p);
        }

        public async Task<bool> UpdatePromotion(PromotionMainDTO dto)
        {
            var p = new Promotion
            {
                PromotionID = dto.PromotionID,
                PromotionName = dto.PromotionName,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsActive = dto.IsActive,
                Percent = dto.Percent,
                Img = dto.Img
            };
            return await _promotionRepository.UpdatePromotion(p);
        }

        public async Task<bool> DeletePromotion(int id)
        {
            return await _promotionRepository.DeletePromotion(id);
        }
        
    }
}
