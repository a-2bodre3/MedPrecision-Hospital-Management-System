using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Entities.Identity
{
    public class Role
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<RolePermission> UserPermission { get; set; } = new List<RolePermission>();
    }
}
