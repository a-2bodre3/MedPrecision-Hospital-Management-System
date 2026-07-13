using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Identity.Role.Commands.UpdateRolePermissions
{
    public record UpdateRolePermissionsCommand : IRequest<bool>
    {
        public required int RoleId { get; set; }
        public required List<int> PermissionIds { get; set; } = new List<int>();
    }
}
