using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Entities.Identity
{
    public class RolePermission
    {
        public required int RoleId { get; set; }
        public Role Role { get; set; }
        public required int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
