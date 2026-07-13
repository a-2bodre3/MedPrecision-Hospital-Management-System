
using HospitalManagement.Domain.Enums;

namespace HospitalManagement.Application.DTO.Structure.Room;

public record UpdateRoomDto : RoomModifybaseDto
{
    public bool IsActive { get; set; }
}