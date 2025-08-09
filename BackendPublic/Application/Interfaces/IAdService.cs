using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAdService
    {
        Task<List<AdMainDTO>> GetAllMainAdDto();
        Task<List<AdDTO>> GetAllAdminAd();
        Task<AdDTO> GetAdById(int id);
        Task<bool> UpdateAd(AdDTO adDTO);
        Task<bool> DeleteAd(int id);
        Task<int> CreateAd(AdDTO adDTO);
    }
}
