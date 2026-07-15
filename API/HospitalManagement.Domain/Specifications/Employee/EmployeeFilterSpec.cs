using System;
using System.Collections.Generic;
using System.Text;
using EmployeeEntity = HospitalManagement.Domain.Entities.Staff.Employee;

namespace HospitalManagement.Domain.Specifications.Employee
{
    public class EmployeeFilterSpec : BaseSpecification<EmployeeEntity>
    {
       
        public EmployeeFilterSpec(string? searchName, int? departmentId, bool? isActive, int pageNumber, int pageSize)
            : base(e =>
                (string.IsNullOrEmpty(searchName) ||
                 e.User.FirstName.ToLower().Contains(searchName.ToLower()) || 
                 e.User.LastName.ToLower().Contains(searchName.ToLower())) &&
                (!departmentId.HasValue || e.DepartmentId == departmentId.Value) &&
                (!isActive.HasValue || e.User.IsActive == isActive.Value))
        {
            AddInclude(e => e.User);
            AddInclude(e => e.Department);
            AddOrderByDescending(emp => emp.HireDate);
            ApplyPaging((pageNumber - 1) * pageSize, pageSize);
        }
    }
}