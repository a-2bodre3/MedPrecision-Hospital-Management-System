using HospitalManagement.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Specifications.Roles
{
    public class RolePermissionsByRoleIdSpec : BaseSpecification<RolePermission>
    {
        public RolePermissionsByRoleIdSpec(int roleId) : base(rp => rp.RoleId == roleId)
        {
            AddInclude(rp => rp.Permission);
        }
    }
}
