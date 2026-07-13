using HospitalManagement.Domain.Enums;

namespace HospitalManagement.Application.DTO.Staff.Doctor;

public record DoctorDetailsDto : StaffInfoDisplayDto
{
    public required string LicenseNumber { get; set; }
    public required decimal ConsultationFee { get; set; }
    public required AcademicDegree Degree { get; set; }
    public required string Specialization { get; set; }
    public required string SubSpecialty { get; set; }
}