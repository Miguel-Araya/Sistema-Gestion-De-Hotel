using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPageService
    {
        Task<List<PageDTO>> GetAllPagesWithImagesDto();
        Task<List<PageDTO>> GetOnePageWithImages(string facilities);
        Task<ResponseDto> DeleteFacility(int facilityId);
        Task<bool> UpdateFacility(int pageID, string pageContent, string imagePath);
        Task<bool> CreateFacility(string contentFacility, string imagePath);
        Task<List<PageAboutUsDTO>> GetPageWithImagesAboutUs(string PageTitle);
        Task<bool> DeleteImagePageAboutUs(int imageID);
        Task<bool> InsertImagePageAboutUs(string ImagePath, int pageID);
        Task<bool> UpdateTextAboutUs(UpdateAboutUsDTO updateAboutUsDTO);
        Task<bool> UpdateContactUs(int pageID, string pageContent);
    }
}
