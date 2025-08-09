using Core.Entities;
using Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPageRepository
    {
        Task<List<Page>> GetAllPagesWithImages();
        Task<List<Page>> GetOnePageWithImages(string facilities);
        Task<Response> DeleteFacility(int facilityId);
        Task<bool> UpdateFacility(int pageID, string pageContent, string imagePath);
        Task<bool> CreateFacility(string contentFacility, string imagePath);
        Task<List<Page>> GetPageWithImagesAboutUs(string PageTitle);
        Task<bool> DeleteImagePageAboutUs(int imageID);
        Task<bool> InsertImagePageAboutUs(string ImagePath, int pageID);
        Task<bool> UpdateTextAboutUs(UpdateAboutUs updateAboutUs);
        Task<bool> UpdateContactUs(int pageID, string pageContent);
    }
}
