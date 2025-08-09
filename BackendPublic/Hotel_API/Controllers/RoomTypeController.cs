using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypeController:ControllerBase 
    {
        private readonly IRoomTypeService _roomTypeService;
        public RoomTypeController(IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
        }
        [HttpGet]
        public async Task<IActionResult> GetRoomType() {
            try
            {
                var roomTypes = await _roomTypeService.GetAllRoomType();
                return Ok(roomTypes);
            }
            catch(Exception ex) {
             return StatusCode(500, $"Ocurrió un error al obtener las páginas: {ex.Message}");

            }
        }

        [HttpGet]
        [Route("getRoomRatePage")]
        public async Task<ActionResult> getRoomRatePage()
        {
            try
            {
                var pages = await _roomTypeService.GetRoomRatePage();
                return Ok(pages);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, $"Ocurrió un error al obtener las páginas: {ex.Message}");
            }
        }


        [HttpPut]
        [Route("UpdateRoomTypeData")]
        public async Task<IActionResult> UpdateRoomTypeData([FromBody] RoomTypeDTO roomTypeDto)
        {
            try
            {
                if (roomTypeDto == null)
                {
                    return BadRequest("Los datos del tipo de habitación son inválidos.");
                }
                var response = await _roomTypeService.UpdateRoomTypeData(roomTypeDto);
                if (response.Code == 0)
                {
                    return Ok(response.Message);
                }
                else
                {
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, $"Ocurrió un error al actualizar el tipo de habitación: {ex.Message}");
            }
        }


        [HttpGet("{roomTypeName}")]
        public async Task<IActionResult> GetRoomType(string roomTypeName)
        {
            var result = await _roomTypeService.GetRoomTypeByName(roomTypeName);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
