using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Staff.Employee.Queries.GetAll
{
    public record EmployeeResponse
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string FullName { get; set; }
        public required string ImageUrl { get; set; }
        public required string JobTitle { get; set; }
        public required bool IsActive { get; set; }
        public required string DepartmentName { get; set; }
    }
}
