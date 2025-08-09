using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {

        private readonly IPromotionService _promotionService;

        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PromotionMainDTO>>> GetAllMainPromotionDto()
        {
            try
            {
                var promotions = await _promotionService.GetAllMainPromotionDto();
                return Ok(promotions);

            }
            catch (Exception ex)
            {
                // Loguear el error (si tienes un sistema de logging, como Serilog, NLog, etc.)
                // Si no tienes un sistema de logs, puedes usar simplemente:
                Console.WriteLine(ex.Message);
                return StatusCode(500, $"Ocurrió un error al obtener las promociones: {ex.Message}");
            }
        }
        
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<PromotionMainDTO>>> GetAll()
        {
            return Ok(await _promotionService.GetAllPromotions());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PromotionMainDTO>> GetById(int id)
        {
            var promo = await _promotionService.GetPromotionById(id);
            if (promo == null) return NotFound();
            return Ok(promo);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] PromotionMainDTO dto)
        {
            var id = await _promotionService.CreatePromotion(dto);
            return Ok(id);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] PromotionMainDTO dto)
        {
            var result = await _promotionService.UpdatePromotion(dto);
            if (!result) return NotFound();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _promotionService.DeletePromotion(id);
            if (!result) return NotFound();
            return Ok();
        }

    }
}
