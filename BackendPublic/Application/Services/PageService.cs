using Application.DTOs;
using Application.Interfaces;
using Core.Interfaces;
using Core.ValueObjects;

namespace Application.Services
{
    public class PageService : IPageService
    {

        private readonly IPageRepository _pageRepository;

        public PageService(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }

        public async Task<ResponseDto> DeleteFacility(int facilityId)
        {
            var response = await _pageRepository.DeleteFacility(facilityId);
            return new ResponseDto
            {
                Code = response.Code,
                Message = response.Message
            };
        }

        // En el servicio (capa de aplicación)
        public async Task<List<PageDTO>> GetAllPagesWithImagesDto()
        {
            var pages = await _pageRepository.GetAllPagesWithImages();

            return pages.Select(p => new PageDTO
            {
                PageID = p.PageID,
                PageTitle = p.PageTitle,
                PageContent = p.PageContent,
                Images = p.PageImages.Select(pi => pi.Image.ImagePath).ToList()
            }).ToList();
        }

        public async Task<List<PageDTO>> GetOnePageWithImages(string facilities)
        {
            var pages = await _pageRepository.GetOnePageWithImages(facilities);

            return pages.Select(p => new PageDTO
            {
                PageID = p.PageID,
                PageTitle = p.PageTitle,
                PageContent = p.PageContent,
                Images = p.PageImages.Select(pi => pi.Image.ImagePath).ToList()
            }).ToList();
        }
        public async Task<bool> UpdateFacility(int pageID, string pageContent, string imagePath)
        {
            bool result = await _pageRepository.UpdateFacility(pageID, pageContent, imagePath);
            return result;
        }

        public async Task<bool> CreateFacility(string contentFacility, string imagePath)
        {
            bool result = await _pageRepository.CreateFacility(contentFacility, imagePath);
            return result;
        }

        public async Task<List<PageAboutUsDTO>> GetPageWithImagesAboutUs(string PageTitle)
        {
            var pages = await _pageRepository.GetPageWithImagesAboutUs(PageTitle);

            return pages.Select(p => new PageAboutUsDTO
            {
                PageID = p.PageID,
                PageTitle = p.PageTitle,
                PageContent = p.PageContent,
                Images = p.PageImages.Select(pi => new ImageDTO
                {
                    ImageID = pi.ImageID,
                    ImagePath = pi.Image.ImagePath
                }).ToList()
            }).ToList();
        }

        public async Task<bool> DeleteImagePageAboutUs(int imageID)
        {
           return await _pageRepository.DeleteImagePageAboutUs(imageID);
        }

        public async Task<bool> InsertImagePageAboutUs(string ImagePath, int pageID)
        {
            return await _pageRepository.InsertImagePageAboutUs(ImagePath, pageID);
        }

        public async Task<bool> UpdateTextAboutUs(UpdateAboutUsDTO updateAboutUsDTO)
        {
            var updateAboutUs = new UpdateAboutUs
            {
                PageID = updateAboutUsDTO.PageID,
                PageContent = updateAboutUsDTO.PageContent,
            };
            return await _pageRepository.UpdateTextAboutUs(updateAboutUs);
        }

        public async Task<bool> UpdateContactUs(int pageID, string pageContent)
        {
            bool result = await _pageRepository.UpdateContactUs(pageID, pageContent);
            return result;
        }

    }
}
