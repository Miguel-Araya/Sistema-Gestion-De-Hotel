using Core.Entities;
using Core.Interfaces;
using Core.ValueObjects;
using Dapper;
using Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using System.Data;

namespace Infrastructure.Data
{
    public class PageRepository : IPageRepository
    {
        private readonly AppDbContext _context;

        public PageRepository(AppDbContext context)
        {
            _context = context;
        }
        //with this methos, we connect to the database
        private SqlConnection CreateConnection()
        {
            var connectionString = _context.Database.GetConnectionString();
            return new SqlConnection(connectionString);
        }
        //With this method, we can get all the pages from the database
        // En el repositorio (capa de infraestructura)
        public async Task<List<Page>> GetAllPagesWithImages()
        {
            using SqlConnection connection = CreateConnection();
            await connection.OpenAsync();

            var pageDictionary = new Dictionary<int, Page>();

            await connection.QueryAsync<Page, Image, Page>(
                "GetPagesWithImages",
                (page, image) =>
                {
                    if (!pageDictionary.TryGetValue(page.PageID, out var existingPage))
                    {
                        existingPage = page;
                        existingPage.PageImages = new List<PageImage>();
                        pageDictionary.Add(existingPage.PageID, existingPage);
                    }

                    if (image != null && image.ImagePath != null)
                    {
                        existingPage.PageImages.Add(new PageImage
                        {
                            PageID = page.PageID,
                            Page = existingPage,
                            ImageID = image.PageImageID,
                            Image = image
                        });
                    }

                    return existingPage;
                },
                splitOn: "ImagePath",  // Ajusta según tu esquema real
                commandType: CommandType.StoredProcedure
            );

            return pageDictionary.Values.ToList();
        }

        public async Task<List<Page>> GetOnePageWithImages(string facilities)
        {
            using SqlConnection connection = CreateConnection();
            await connection.OpenAsync();

            var pageDictionary = new Dictionary<int, Page>();

            await connection.QueryAsync<Page, Image, Page>(
               "sp_get_facilities",
               (page, image) =>
               {
                   if (!pageDictionary.TryGetValue(page.PageID, out var existingPage))
                   {
                       existingPage = page;
                       existingPage.PageImages = new List<PageImage>();
                       pageDictionary.Add(existingPage.PageID, existingPage);
                   }

                   if (image != null && !string.IsNullOrEmpty(image.ImagePath))
                   {
                       existingPage.PageImages.Add(new PageImage
                       {
                           PageID = page.PageID,
                           Page = existingPage,
                           ImageID = image.PageImageID,
                           Image = image
                       });
                   }

                   return existingPage;
               },
               param: new { PageTitle = facilities },
               splitOn: "ImagePath",
               commandType: CommandType.StoredProcedure
           );
            return pageDictionary.Values.ToList();
        }

        public async Task<Response> DeleteFacility(int facilityId)
        {
            using SqlConnection connection = CreateConnection();
            await connection.OpenAsync();

            using SqlCommand command = new SqlCommand("sp_deleteFacility", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@PageID", facilityId);

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            Response response = new Response();

            if (await reader.ReadAsync())
            {
                response.Code = reader.GetInt32(reader.GetOrdinal("CodigoResultado"));
                response.Message = reader.GetString(reader.GetOrdinal("Mensaje"));
            }
            else
            {
                // En caso de que no retorne nada, aunque no debería pasar
                response.Code = -1;
                response.Message = "No se recibió respuesta del procedimiento.";
            }

            return response;
        }

        public async Task<bool> UpdateFacility(int pageId, string pageContent, string imagePath)
        {
            try
            {
                using SqlConnection connection = CreateConnection();
                await connection.OpenAsync();

                using SqlCommand command = new SqlCommand("sp_updateFacility", connection);
                command.CommandType = CommandType.StoredProcedure;
          

                command.Parameters.AddWithValue("@PageID", pageId);
                command.Parameters.AddWithValue("@PageContent", pageContent);
                command.Parameters.AddWithValue("@ImagePath", imagePath);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                // Si se afectó al menos 1 fila = éxito
                return rowsAffected > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CreateFacility(string contentFacility, string imagePath)
        {
            try {
                using SqlConnection connection = CreateConnection();
                await connection.OpenAsync();

                using SqlCommand command = new SqlCommand("sp_addFaclity", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@PageContent", contentFacility);
                command.Parameters.AddWithValue("@ImagePath", imagePath);

                int rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
            catch{
                return false;
            }
        }

        public async Task<List<Page>> GetPageWithImagesAboutUs(string PageTitle)
        {
            using SqlConnection connection = CreateConnection();
            await connection.OpenAsync();

            var pageDictionary = new Dictionary<int, Page>();

            await connection.QueryAsync<Page, Image, Page>(
               "sp_GetPageWithImagesAboutUs",
               (page, image) =>
               {
                   if (!pageDictionary.TryGetValue(page.PageID, out var existingPage))
                   {
                       existingPage = page;
                       existingPage.PageImages = new List<PageImage>();
                       pageDictionary.Add(existingPage.PageID, existingPage);
                   }

                   if (image != null && !string.IsNullOrEmpty(image.ImagePath))
                   {
                       existingPage.PageImages.Add(new PageImage
                       {
                           PageID = page.PageID,
                           Page = existingPage,
                           ImageID = image.PageImageID,
                           Image = image
                       });
                   }

                   return existingPage;
               },
               param: new { PageTitle = PageTitle },
               splitOn: "ImagePath",
               commandType: CommandType.StoredProcedure
           );
            return pageDictionary.Values.ToList();
        }

        public async Task<bool> DeleteImagePageAboutUs(int imageID)
        {
            try
            {
                using SqlConnection connection = CreateConnection();
                await connection.OpenAsync();

                using SqlCommand command = new SqlCommand("sp_DeleteImagePageAboutUs", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ImageID", imageID);

                using SqlDataReader reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    var resultMessage = reader["Message"]?.ToString();
                    return resultMessage == "Image deleted successfully.";
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> InsertImagePageAboutUs(string ImagePath, int pageID)
        {
            try
            {
                using SqlConnection connection = CreateConnection();
                await connection.OpenAsync();

                using SqlCommand command = new SqlCommand("sp_InsertImagePageAboutUs", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ImagePath", ImagePath);
                command.Parameters.AddWithValue("@PageID", pageID);

                using SqlDataReader reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    var resultMessage = reader["Message"]?.ToString();
                    return resultMessage == "Image inserted successfully.";
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateTextAboutUs(UpdateAboutUs updateAboutUs)
        {
            try
            {
                using SqlConnection connection = CreateConnection();
                await connection.OpenAsync();

                using SqlCommand command = new SqlCommand("sp_UpdateTextAboutUs", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@PageID", updateAboutUs.PageID);
                command.Parameters.AddWithValue("@PageContent", updateAboutUs.PageContent);

                int rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateContactUs(int pageID, string pageContent)
        {
            try
            {
                using SqlConnection connection = CreateConnection();
                await connection.OpenAsync();

                using SqlCommand command = new SqlCommand("sp_updateContactUs", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@PageID", pageID);
                command.Parameters.AddWithValue("@PageContent", pageContent);

                int rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
