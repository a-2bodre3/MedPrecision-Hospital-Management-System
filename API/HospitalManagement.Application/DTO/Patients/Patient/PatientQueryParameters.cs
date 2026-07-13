namespace HospitalManagement.Application.DTO.Patients.Patient;

public record PatientQueryParameters(
    string? SearchTerm = null,  
    int PageNumber = 1,          
    int PageSize = 10   
);