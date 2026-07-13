using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Identity.Role.Queries.GetRoles
{
    public record RolesQuery : IRequest<List<RoleResponse>>;
}
