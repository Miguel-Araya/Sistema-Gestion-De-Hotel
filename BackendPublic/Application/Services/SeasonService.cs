using Application.DTOs;
using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;

namespace Application.Services;

public class SeasonService : ISeasonService
    {
        private readonly ISeasonRepository _seasonRepository;

        public SeasonService(ISeasonRepository seasonRepository)
        {
            _seasonRepository = seasonRepository;
        }

        public async Task<List<SeasonDTO>> GetAllSeasons()
        {
            var seasons = await _seasonRepository.GetAllSeasons();
            return seasons.Select(s => new SeasonDTO
            {
                SeasonID = s.SeasonID,
                SeasonName = s.SeasonName,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                Percent = s.Percent,
                IsActive = s.IsActive,
                IsHigh = s.IsHigh
            }).ToList();
        }

        public async Task<SeasonDTO> GetSeasonById(int id)
        {
            var s = await _seasonRepository.GetSeasonById(id);
            return s == null ? null : new SeasonDTO
            {
                SeasonID = s.SeasonID,
                SeasonName = s.SeasonName,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                Percent = s.Percent,
                IsActive = s.IsActive,
                IsHigh = s.IsHigh
            };
        }

        public async Task<bool> CreateSeason(SeasonDTO seasonDto)
        {
            var season = new Season
            {
                SeasonName = seasonDto.SeasonName,
                StartDate = seasonDto.StartDate,
                EndDate = seasonDto.EndDate,
                Percent = seasonDto.Percent,
                IsActive = seasonDto.IsActive,
                IsHigh = seasonDto.IsHigh
            };

            return await _seasonRepository.CreateSeason(season);
        }

        public async Task<bool> UpdateSeason(SeasonDTO seasonDto)
        {
            var season = new Season
            {
                SeasonID = seasonDto.SeasonID,
                SeasonName = seasonDto.SeasonName,
                StartDate = seasonDto.StartDate,
                EndDate = seasonDto.EndDate,
                Percent = seasonDto.Percent,
                IsActive = seasonDto.IsActive,
                IsHigh = seasonDto.IsHigh
            };

            return await _seasonRepository.UpdateSeason(season);
        }

        public async Task<bool> DeleteSeason(int id)
        {
            return await _seasonRepository.DeleteSeason(id);
        }
    }
    