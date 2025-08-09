using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdController : ControllerBase
    {
        private readonly IAdService _adService;

        public AdController(IAdService adService)
        {
            _adService = adService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdMainDTO>>> GetPages()
        {
            try
            {
                var pages = await _adService.GetAllMainAdDto();
                return Ok(pages);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, $"Ocurrió un error al obtener los anuncios: {ex.Message}");
            }

        }

        [HttpGet]
        [Route("GetAdminAd")]
        public async Task<ActionResult<IEnumerable<AdDTO>>> GetAdminAd()
        {
            try
            {
                var pages = await _adService.GetAllAdminAd();
                return Ok(pages);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, $"Ocurrió un error al obtener los anuncios: {ex.Message}");
            }

        }

        [HttpGet]
        [Route("GetAdById")]
        public async Task<ActionResult<IEnumerable<AdDTO>>> GetAdById(int id)
        {
            try
            {
                var pages = await _adService.GetAdById(id);
                return Ok(pages);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, $"Ocurrió un error al obtener los anuncios: {ex.Message}");
            }

        }

        [HttpDelete]
        [Route("DeleteAd")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _adService.DeleteAd(id);
            if (!result) return NotFound();
            return Ok();
        }

        [HttpPut]
        [Route("UpdateAd")]
        public async Task<ActionResult> Update([FromBody] AdDTO adDTO)
        {
            var result = await _adService.UpdateAd(adDTO);
            if (!result) return NotFound();
            return Ok();
        }

        [HttpPost]
        [Route("CreateAd")]
        public async Task<ActionResult<int>> Create([FromBody] AdDTO dto)
        {
            var id = await _adService.CreateAd(dto);
            return Ok(id);
        }

    }
}
