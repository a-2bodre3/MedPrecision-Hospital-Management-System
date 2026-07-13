using HospitalManagement.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using DoctorEntity = HospitalManagement.Domain.Entities.Staff.Doctor;
using EmployeeEntity = HospitalManagement.Domain.Entities.Staff.Employee;

namespace HospitalManagement.Domain.Specifications.Doctor
{
    public class DoctorDetailsSpec : BaseSpecification<DoctorEntity>
    {
        public DoctorDetailsSpec(int Id) : base(d => d.Id == Id)
        {
            AddInclude(d => d.Employee);
            AddInclude($"{nameof(DoctorEntity.Employee)}.{nameof(EmployeeEntity.User)}");
            AddInclude($"{nameof(DoctorEntity.Employee)}.{nameof(EmployeeEntity.Address)}");
            AddInclude($"{nameof(DoctorEntity.Employee)}.{nameof(EmployeeEntity.Department)}");
            AddInclude($"{nameof(DoctorEntity.Employee)}.{nameof(EmployeeEntity.User)}.{nameof(User.UserBranches)}.{nameof(UserBranch.Branch)}");
            AddInclude($"{nameof(DoctorEntity.Employee)}.{nameof(EmployeeEntity.User)}.{nameof(User.UserRoles)}.{nameof(UserRole.Role)}");
            AddInclude(d => d.SubSpecialty);
            AddInclude($"{nameof(DoctorEntity.SubSpecialty)}.Specialization");
        }
    }
}
