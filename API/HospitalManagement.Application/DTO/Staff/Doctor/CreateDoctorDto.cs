using HospitalManagement.Domain.Enums;

namespace HospitalManagement.Application.DTO.Staff.Doctor;

public record CreateDoctorDto : StaffInfoModifyCreateDto
{
    public required string LicenseNumber { get; set; }
    public required decimal ConsultationFee { get; set; }
    public required AcademicDegree Degree { get; set; }
    public required int SubSpecialtyId { get; set; }
}