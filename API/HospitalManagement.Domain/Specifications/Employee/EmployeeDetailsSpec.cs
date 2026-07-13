using HospitalManagement.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using EmployeeEntity = HospitalManagement.Domain.Entities.Staff.Employee;

namespace HospitalManagement.Domain.Specifications.Employee
{
    public class EmployeeDetailsSpec : BaseSpecification<EmployeeEntity>
    {
        public EmployeeDetailsSpec(int employeeId) : base(e => e.Id == employeeId)
        {
            
            AddInclude(e => e.Department);
            AddInclude(e => e.Address!);
            AddInclude(e => e.User);
            AddInclude($"{nameof(EmployeeEntity.User)}.{nameof(User.UserBranches)}.{nameof(UserBranch.Branch)}");
            AddInclude($"{nameof(EmployeeEntity.User)}.{nameof(User.UserRoles)}.{nameof(UserRole.Role)}");
        }
    }
}
