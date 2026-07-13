using HospitalManagement.Domain.Entities.Common;
using HospitalManagement.Domain.Entities.Identity;
using HospitalManagement.Domain.Entities.Structure;
using HospitalManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Entities.Staff
{
    public class Employee
    {
        public int Id { get; set; }
        public required string JobTitle { get; set; }
        public decimal Salary { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime HireDate { get; set; } = DateTime.UtcNow;
        public Gender Gender { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public Doctor? Doctor { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public int? AddressId { get; set; }
        public Address? Address { get; set; }
    }
}
