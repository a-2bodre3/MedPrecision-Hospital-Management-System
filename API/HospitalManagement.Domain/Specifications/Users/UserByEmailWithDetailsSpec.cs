using HospitalManagement.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Specifications.Users
{
    public class UserByEmailWithDetailsSpec : BaseSpecification<User>
    {
        public UserByEmailWithDetailsSpec(string email) : base(u => u.Email == email)
        {
            AddInclude("UserRoles.Role");
            AddInclude("UserBranches.Branch");
        }
    }
}
