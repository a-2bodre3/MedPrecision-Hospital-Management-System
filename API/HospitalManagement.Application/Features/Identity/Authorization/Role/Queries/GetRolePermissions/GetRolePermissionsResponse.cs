using HospitalManagement.Application.DTO.Identity.Role;
using HospitalManagement.Application.Features.Identity.Role.Queries.GetRoles;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Identity.Role.Queries.GetRolePermissions
{
    public record RolePermissionsResponse : RoleResponse
    {
        public List<PermissionDto> Permissions { get; set; } = new List<PermissionDto>();
    }
}
