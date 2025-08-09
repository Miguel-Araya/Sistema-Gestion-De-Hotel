using Application.DTOs;
using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AdService : IAdService
    {

        private readonly IAdRepository _adRepository;

        public AdService(IAdRepository adRepository)
        {
            _adRepository = adRepository;
        }

        public async Task<int> CreateAd(AdDTO dto)
        {
            var ad = new Ad
            {
                AdID = dto.AdID,
                Name = dto.Name,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsActive = dto.IsActive,
                Img = dto.Img,
                ImgUrl = dto.ImgUrl
            };

            return await _adRepository.CreateAd(ad);

        }

        public async Task<bool> DeleteAd(int id)
        {
            return await _adRepository.DeleteAd(id);
        }

        public async Task<AdDTO> GetAdById(int id)
        {
            var ad = await _adRepository.GetAdById(id);
            return new AdDTO
            {
                AdID = ad.AdID,
                Name = ad.Name,
                StartDate = ad.StartDate,
                EndDate = ad.EndDate,
                IsActive = ad.IsActive,
                Img = ad.Img,
                ImgUrl = ad.ImgUrl,
            };

        }

        public async Task<List<AdDTO>> GetAllAdminAd()
        {
            var adAdmin = await _adRepository.GetAllAdminAd();
            
            return adAdmin.Select(ad => new AdDTO
            {
                AdID = ad.AdID,
                Name = ad.Name,
                StartDate = ad.StartDate,
                EndDate = ad.EndDate,
                IsActive = ad.IsActive,
                Img = ad.Img,
                ImgUrl = ad.ImgUrl,
            }).ToList();
        }

        public async Task<List<AdMainDTO>> GetAllMainAdDto()
        {

            var adMain = await _adRepository.GetAllMainAd();
               
            return adMain.Select(ad => new AdMainDTO
            {
                AdID = ad.AdID,
                Name = ad.Name,
                Img = ad.Img,
                ImgUrl = ad.ImgUrl,
            }).ToList();


        }

        public async Task<bool> UpdateAd(AdDTO adDTO)
        {
            var ad = new Ad
            {
                AdID = adDTO.AdID,
                Name = adDTO.Name,
                StartDate = adDTO.StartDate,
                EndDate = adDTO.EndDate,
                IsActive = adDTO.IsActive,
                Img = adDTO.Img,
                ImgUrl = adDTO.ImgUrl
            };

            return await _adRepository.UpdateAd(ad);
        }

    }
}
