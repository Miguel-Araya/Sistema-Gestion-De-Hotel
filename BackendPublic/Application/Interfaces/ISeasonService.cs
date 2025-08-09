using Application.DTOs;

namespace Application.Interfaces;

public interface ISeasonService
{
    Task<List<SeasonDTO>> GetAllSeasons();
    Task<SeasonDTO> GetSeasonById(int id);
    Task<bool> CreateSeason(SeasonDTO seasonDto);
    Task<bool> UpdateSeason(SeasonDTO seasonDto);
    Task<bool> DeleteSeason(int id);
}