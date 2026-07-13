using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Identity.Role.Queries.GetRolePermissions
{
    public record RolePermissionsQuery(int RoleId) : IRequest<RolePermissionsResponse>;
    
}
