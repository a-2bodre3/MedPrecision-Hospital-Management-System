using HospitalManagement.Domain.Entities.Staff;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Entities.Structure
{
    public class Department
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
