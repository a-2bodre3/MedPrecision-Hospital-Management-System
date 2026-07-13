using HospitalManagement.Domain.Entities.Structure;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Entities.Identity
{
    public class UserBranch
    {
        public required int UserId { get; set; }
        public User User { get; set; }
        public required int BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}
