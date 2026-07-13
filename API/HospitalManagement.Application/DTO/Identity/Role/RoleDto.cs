namespace HospitalManagement.Application.DTO.Identity.Role;

public record RoleDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
}