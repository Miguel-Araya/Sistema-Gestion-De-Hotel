using Application.DTOs;
using Core.Entities;
using Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRoomTypeService
    {
        Task<List<RoomTypeDTO>> GetAllRoomType();
        Task<List<RoomTypeDTO>> GetRoomRatePage();
        Task<ResponseDto> UpdateRoomTypeData(RoomTypeDTO roomTypedto);
        Task<RoomTypeDTO?> GetRoomTypeByName(string roomTypeName);
    }
}
