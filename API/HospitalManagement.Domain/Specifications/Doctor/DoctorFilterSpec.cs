using System;
using System.Collections.Generic;
using System.Text;
using DoctorEntity = HospitalManagement.Domain.Entities.Staff.Doctor;
using EmployeeEntity = HospitalManagement.Domain.Entities.Staff.Employee;

namespace HospitalManagement.Domain.Specifications.Doctor
{
    public class DoctorFilterSpec : BaseSpecification<DoctorEntity>
    {
        public DoctorFilterSpec(string? searchName, int? departmentId, bool? isActive, int pageNumber, int pageSize)
            :base(e => 
            (string.IsNullOrEmpty(searchName) ||
            e.Employee.User.FirstName.ToLower().Contains(searchName.ToLower()) ||
            e.Employee.User.LastName.ToLower().Contains(searchName.ToLower())) && 
            (!departmentId.HasValue || e.Employee.DepartmentId == departmentId.Value) &&
            (!isActive.HasValue || e.Employee.User.IsActive == isActive.Value))
        {
            AddInclude(d => d.Employee);
            AddInclude($"{nameof(DoctorEntity.Employee)}.{nameof(EmployeeEntity.User)}");
            AddInclude($"{nameof(DoctorEntity.Employee)}.{nameof(EmployeeEntity.Department)}");
            AddOrderByDescending(d => d.Employee.HireDate);
            ApplyPaging((pageNumber - 1) * pageSize , pageSize);
        }
    }
}
