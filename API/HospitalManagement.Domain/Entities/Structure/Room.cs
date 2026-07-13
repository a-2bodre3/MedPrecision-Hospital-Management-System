using HospitalManagement.Domain.Entities.Scheduling;
using HospitalManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Entities.Structure
{
    public class Room
    {
        public int Id { get; set; }
        public required string RoomNumber { get; set; }
        public int Floor { get; set; }
        public RoomType RoomType { get; set; }
        public int Capacity { get; set; } = 1;
        public bool IsActive { get; set; } = true;
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public ICollection<DoctorSchedule> DoctorSchedules { get; set; } = new List<DoctorSchedule>();
    }
}
