using HospitalManagement.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using RoleEntity = HospitalManagement.Domain.Entities.Identity.Role;

namespace HospitalManagement.Domain.Specifications.Roles
{
    public class RoleWithPermissionsSpec : BaseSpecification<RoleEntity>
    {
        public RoleWithPermissionsSpec(int roleId) : base(r => r.Id == roleId)
        {
            AddInclude(r => r.UserPermission);
            AddInclude($"{nameof(Role.UserPermission)}.{nameof(RolePermission.Permission)}");
        }
    }
}
