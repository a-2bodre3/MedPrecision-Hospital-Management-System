using HospitalManagement.Domain.Enums;
using HospitalManagement.Domain.Value_Objects;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Staff.Employee.Commands.Update
{
    public record UpdateEmployeeCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public IFormFile? ImageFile { get; set; }
        public required decimal Salary { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required string JobTitle { get; set; }
        public required Gender Gender { get; set; }
        public required Address Address { get; set; }
        public required int BranchId { get; set; }
        public required int RoleId { get; set; }
        public required int DepartmentId { get; set; }
    }
}
