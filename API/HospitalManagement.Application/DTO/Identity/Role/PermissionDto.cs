namespace HospitalManagement.Application.DTO.Identity.Role
{
    public record PermissionDto
    {
        public int Id { get; set; }
        public required string Token { get; set; }
        public required string Description { get; set; }
        public required string Module { get; set; }
    }
}