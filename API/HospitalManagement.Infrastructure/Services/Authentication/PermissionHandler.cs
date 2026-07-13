using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace HospitalManagement.Infrastructure.Services.Authentication
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {

            var userPermissions = context.User.Claims
                .Where(c => c.Type == "Permissions")
                .Select(c => c.Value)
                .ToList();


            var hasAnyPermission = requirement.RequiredPermissions.Any(p => userPermissions.Contains(p));

            if (hasAnyPermission)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
