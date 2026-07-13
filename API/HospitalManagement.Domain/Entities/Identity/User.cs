using HospitalManagement.Domain.Entities.Patients;
using HospitalManagement.Domain.Entities.Staff;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Entities.Identity
{
    public class User
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; } = true;
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<UserBranch> UserBranches { get; set; } = new List<UserBranch>();

        public Employee? Employee { get; set; }
        public Patient? Patient { get; set; }
    }
}
