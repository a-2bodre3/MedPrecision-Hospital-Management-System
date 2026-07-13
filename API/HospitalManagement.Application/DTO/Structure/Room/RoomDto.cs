

using HospitalManagement.Domain.Enums;

namespace HospitalManagement.Application.DTO.Structure.Room;

public record RoomDto(
    int Id,
    string RoomNumber,
    int Floor,
    int Capacity,
    RoomType RoomType,
    bool IsActive,
    string DepartmentName,
    string BranchName
    );