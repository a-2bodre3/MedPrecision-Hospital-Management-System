using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Entities.Identity
{
    public class UserRole
    {
        public required int UserId { get; set; }
        public User User { get; set; }
        public required int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
