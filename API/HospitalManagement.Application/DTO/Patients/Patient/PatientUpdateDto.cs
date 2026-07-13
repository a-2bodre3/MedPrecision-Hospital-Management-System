namespace HospitalManagement.Application.DTO.Patients.Patient;

public record PatientUpdateDto : PatientModifyBasicDto
{
    public bool IsActive { get; set; }
}