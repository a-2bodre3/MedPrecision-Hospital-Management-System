using HospitalManagement.Domain.Entities.Common;
using HospitalManagement.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Entities.Structure
{
    public class Branch
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public required string PhoneNumber { get; set; }
        public bool IsActive { get; set; } = true;
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public ICollection<UserBranch> UserBranches { get; set; } = new List<UserBranch>();
        public ICollection<Department> Departments { get; set; } = new List<Department>();
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
