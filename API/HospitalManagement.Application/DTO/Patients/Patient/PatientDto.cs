namespace HospitalManagement.Application.DTO.Patients.Patient;


public record PatientDto 
{
    public required int Id { get; set; }
    public required string Email { get; set; }
    public required string FullName { get; set; }
    public required string ImageUrl { get; set; }
    public required string PatientCode { get; set; }
    public bool IsActive { get; set; }
    public required string PhoneNumber { get; set; }
}