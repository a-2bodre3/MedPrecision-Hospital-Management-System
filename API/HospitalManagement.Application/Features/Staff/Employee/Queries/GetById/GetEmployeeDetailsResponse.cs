using HospitalManagement.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Staff.Employee.Queries.GetById
{
    public record EmployeeDetailsResponse
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string FullName { get; set; }
        public required string ImageUrl { get; set; }
        public required string JobTitle { get; set; }
        public bool IsActive { get; set; }
        public required string DepartmentName { get; set; }
        public required string PhoneNumber { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required string RoleName { get; set; }
        public required string BranchName { get; set; }
        public required decimal Salary { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public required Address Address { get; set; }
    }
}
