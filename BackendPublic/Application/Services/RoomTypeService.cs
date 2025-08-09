using Application.DTOs;
using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        public RoomTypeService(IRoomTypeRepository roomTypeRepository)
        {
            _roomTypeRepository = roomTypeRepository;
        }
        public async Task<List<RoomTypeDTO>> GetAllRoomType()
        {
           var roomTypes = await _roomTypeRepository.GetAllRoomType();
           return roomTypes.Select(r=> new RoomTypeDTO
           {
               RoomTypeID = r.RoomTypeId,
               RoomTypeName = r.RoomTypeName,
           }).ToList();
        }
        public async Task<List<RoomTypeDTO>> GetRoomRatePage()
        {
            var roomTypes = await _roomTypeRepository.GetAllRoomTypeBySeason();

            return roomTypes.Select(r => new RoomTypeDTO
            {
                RoomTypeID = r.RoomTypeId,
                RoomTypeName = r.RoomTypeName,
                Price = r.Price,
                Characteristics = r.Characteristics,
                Description = r.description,
                Image = r.Image,
            }).ToList();

        }

        public async Task<RoomTypeDTO?> GetRoomTypeByName(string roomTypeName)
        {
            var roomType = await _roomTypeRepository.GetRoomTypeByName(roomTypeName);
            if (roomType == null)
            {
                return await Task.FromResult<RoomTypeDTO?>(null);
            }
            return await Task.FromResult(new RoomTypeDTO
            {
                RoomTypeID = roomType.RoomTypeId,
                RoomTypeName = roomType.RoomTypeName,
                Price = roomType.Price,
                Characteristics = roomType.Characteristics,
                Description = roomType.description,
                Image = roomType.Image,
            });
        }
        public async Task<ResponseDto> UpdateRoomTypeData(RoomTypeDTO roomTypedto)
        {
            RoomType roomType = new RoomType
            {
                RoomTypeId = roomTypedto.RoomTypeID,
                RoomTypeName = roomTypedto.RoomTypeName,
                Price = roomTypedto.Price,
                Characteristics = roomTypedto.Characteristics,
                description = roomTypedto.Description,
                Image = roomTypedto.Image
            };

            var response = await _roomTypeRepository.UpdateRoomTypeData(roomType);

            return new ResponseDto
            {
                Code = response.Code,
                Message = response.Message
            };
        }
    }
}
