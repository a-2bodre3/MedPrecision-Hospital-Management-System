using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Entities.Identity
{
    public class Permission
    {
        public int Id { get; set; }
        public required string Token { get; set; }
        public required string Description { get; set; }
        public required string Module { get; set; }
        public ICollection<RolePermission> RolePermission { get; set; } = new List<RolePermission>();
    }
}
