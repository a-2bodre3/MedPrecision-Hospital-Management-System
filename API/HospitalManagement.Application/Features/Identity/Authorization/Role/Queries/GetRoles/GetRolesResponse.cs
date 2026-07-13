using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Identity.Role.Queries.GetRoles
{
    public record RoleResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
