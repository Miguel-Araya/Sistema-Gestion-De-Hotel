using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class PageController : ControllerBase
    {
        private readonly IPageService _pageService;
        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }
        [HttpGet]
        public async Task<IActionResult> GetPages()
        {
            try
            {
                var pages = await _pageService.GetAllPagesWithImagesDto();

                if (pages == null || !pages.Any())
                {
                    return NotFound("No se encontraron páginas.");
                }

                return Ok(pages);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}"); // Solo imprime en consola (temporal)
                return Problem(detail: "Ocurrió un error interno. Inténtelo más tarde.", statusCode: 500);
            }
        }
        [HttpGet]
        [Route("getPageForTittle")]
        public async Task<ActionResult<IEnumerable<PageDTO>>> getPageForTittle(string facilities)
        {
            try
            {
                var pages = await _pageService.GetOnePageWithImages(facilities);
                return Ok(pages);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, $"Ocurrió un error al obtener las páginas: {ex.Message}");
            }
        }
        [HttpDelete("facility/{id}")]
        public async Task<IActionResult> DeleteFacility(int id)
        {
            try
            {
                var result = await _pageService.DeleteFacility(id);
                if (result.Code == 0)
                {
                    return Ok(new { message = result.Message });
                }

                return NotFound(new { message = "Facilidad no encontrada." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error interno. Inténtelo más tarde." });
            }
        }
        [HttpPut("updateFacility")]
        public async Task<IActionResult> UpdateFacility(int pageID, string pageContent, string imagePath)
        {
            try
            {
                bool result = await _pageService.UpdateFacility(pageID, pageContent, imagePath);
                if (result)
                {
                    return Ok(new { message = "Facilidad actualizada correctamente." });
                }
                return NotFound(new { message = "Facilidad no encontrada." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error interno. Inténtelo más tarde." });
            }

        }
        [HttpPost("createFacility")]
        public async Task<IActionResult> CreateFacility(string contentFacility, string imagePath)
        {
            try
            {
                bool result = await _pageService.CreateFacility(contentFacility, imagePath);
                if (result)
                {
                    return Ok(new { message = "Facilidad creada correctamente." });
                }
                return BadRequest(new { message = "Error al crear la facilidad." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error interno. Inténtelo más tarde." });
            }
        }
        [HttpGet("GetPageWithImagesAboutUs")]
        public async Task<IActionResult> GetPageWithImagesAboutUs(string PageTitle)
        {
            var result = await _pageService.GetPageWithImagesAboutUs(PageTitle);

            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron datos para el título especificado.");
            }

            return Ok(result);
        }
        [HttpDelete("DeleteImagePageAboutUs")]
        public async Task<IActionResult> DeleteImagePageAboutUs(int imageID)
        {
            bool result= await _pageService.DeleteImagePageAboutUs(imageID);
            if (result)
                return Ok();
            else
                return BadRequest();
        }
        [HttpPost("InsertImagePageAboutUs")]
        public async Task<IActionResult> InsertImagePageAboutUs(string ImagePath, int PageID)
        {
            bool result = await _pageService.InsertImagePageAboutUs(ImagePath, PageID);
            if (result)
                return Ok();
            else
                return BadRequest();
        }
        [HttpPut("UpdateTextAboutUs")]
        public async Task<IActionResult> UpdateTextAboutUs(UpdateAboutUsDTO updateAboutUsDTO)
        {
            bool result = await _pageService.UpdateTextAboutUs(updateAboutUsDTO);
            if (result)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut("UpdateContactUs")]
        public async Task<IActionResult> UpdateContactUs(UpdateContactUsDTO updateContactUsDTO /*int pageID, string pageContentint pageID, string phone1, string phone2, string poBox, string email, string facebook, string instagram*/)
        {
            try
            {
                bool result = await _pageService.UpdateContactUs(updateContactUsDTO.PageID, updateContactUsDTO.PageContent);
                if (result)
                {
                    return Ok(new { message = "Contacto actualizado correctamente." });
                }
                return NotFound(new { message = "Registro de contacto no encontrado." });

                //string content = $"{phone1};{phone2};{poBox};{email};{facebook};{instagram}";
                //bool result = await _pageService.UpdateFacility(pageID, content, null); // null porque ContactUs no usa imagePath
                //if (result)
                //{
                //    return Ok(new { message = "Contacto actualizado correctamente." });
                //}
                //return NotFound(new { message = "Registro de contacto no encontrado." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error interno. Inténtelo más tarde." });
            }
        }

    }

}
