using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAdRepository
    {
        Task<List<Ad>> GetAllMainAd();
        Task<List<Ad>> GetAllAdminAd();
        Task<Ad> GetAdById(int id);
        Task<bool> UpdateAd(Ad ad);
        Task<bool> DeleteAd(int id);
        Task<int> CreateAd(Ad ad);
    }
}
