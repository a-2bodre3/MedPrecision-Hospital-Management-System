using HospitalManagement.Application.DTO.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.DTO.Staff
{
    public abstract record StaffInfoModifyDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public IFormFile? ImageFile { get; set; }
        public required int RoleId { get; set; }
        public required int BranchId { get; set; }
        public required string JobTitle { get; set; }
        public required int DepartmentId { get; set; }
        public required decimal Salary { get; set; }

        public required DateTime DateOfBirth { get; set; }
        public required string Gender { get; set; }
        public required AddressDto Address { get; set; }
    }

    public abstract record StaffInfoModifyCreateDto : StaffInfoModifyDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public abstract record StaffInfoModifyUpdateDto : StaffInfoModifyDto
    {
        public bool IsActive { get; set; }
    }
}