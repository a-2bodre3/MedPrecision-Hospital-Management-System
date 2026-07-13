using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Identity.Authorization.Permission.Queries.GetPermissions
{
    public record PermissionsQuery : IRequest<List<PermissionsResponse>>;
}
