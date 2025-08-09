using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonController : ControllerBase
    {
        private readonly ISeasonService _seasonService;

        public SeasonController(ISeasonService seasonService)
        {
            _seasonService = seasonService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeasonDTO>>> GetSeasons()
        {
            var seasons = await _seasonService.GetAllSeasons();
            return Ok(seasons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SeasonDTO>> GetSeason(int id)
        {
            var season = await _seasonService.GetSeasonById(id);
            if (season == null)
                return NotFound();
            return Ok(season);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSeason([FromBody] SeasonDTO seasonDto)
        {
            var result = await _seasonService.CreateSeason(seasonDto);
            return result ? Ok() : BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSeason([FromBody] SeasonDTO seasonDto)
        {
            var result = await _seasonService.UpdateSeason(seasonDto);
            return result ? Ok() : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeason(int id)
        {
            var result = await _seasonService.DeleteSeason(id);
            return result ? Ok() : NotFound();
        }
    }
}