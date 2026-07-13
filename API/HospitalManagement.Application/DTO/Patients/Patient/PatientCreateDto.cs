using Microsoft.AspNetCore.Http;

namespace HospitalManagement.Application.DTO.Patients.Patient
{
    public record PatientCreateDto : PatientModifyBasicDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public  DateTime DateOfBirth { get; set; }
    }
}