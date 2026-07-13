using Microsoft.AspNetCore.Http;

namespace HospitalManagement.Application.DTO.Staff.Doctor;

public record UpdateDoctorDto : StaffInfoModifyUpdateDto
{
    public required decimal ConsultationFee { get; set; }
}