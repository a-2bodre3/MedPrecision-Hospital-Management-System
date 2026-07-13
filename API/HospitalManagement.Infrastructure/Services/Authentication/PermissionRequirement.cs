using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Infrastructure.Services.Authentication
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string[] RequiredPermissions { get; }

        public PermissionRequirement(string[] requiredPermissions)
        {
            RequiredPermissions = requiredPermissions;
        }
    }
}
