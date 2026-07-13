namespace HospitalManagement.Application.DTO.Staff.Employee;

public record EmployeeQueryParameters(
    string? SearchTerm = null,  
    int? RoleId = null,        
    int? DepartmentId = null,    
    int PageNumber = 1,          
    int PageSize = 10            
);