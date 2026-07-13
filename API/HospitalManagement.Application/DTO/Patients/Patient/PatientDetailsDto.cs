using HospitalManagement.Application.DTO.Common;

namespace HospitalManagement.Application.DTO.Patients.Patient;


public record PatientDetailsDto : PatientDto 
{
    public DateTime CreatedAt { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public required String Gender { get; set; }
    public string? EmergencyPhoneNumber { get; set; }
    public string? EmergencyEmail { get; set; }
    public required AddressDto Address { get; init; }

    public List<string>? Allergies { get; set; }
    public List<string>? ChronicDiseases { get; set; }

}