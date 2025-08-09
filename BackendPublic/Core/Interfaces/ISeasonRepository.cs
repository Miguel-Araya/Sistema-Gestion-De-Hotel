using Core.Entities;

namespace Core.Interfaces;

public interface ISeasonRepository
{
    Task<List<Season>> GetAllSeasons();
    Task<Season> GetSeasonById(int id);
    Task<bool> CreateSeason(Season season);
    Task<bool> UpdateSeason(Season season);
    Task<bool> DeleteSeason(int id);
}