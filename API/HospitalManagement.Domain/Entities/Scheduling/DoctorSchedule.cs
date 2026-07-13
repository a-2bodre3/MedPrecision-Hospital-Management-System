using HospitalManagement.Domain.Entities.Common;
using HospitalManagement.Domain.Entities.Staff;
using HospitalManagement.Domain.Entities.Structure;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace HospitalManagement.Domain.Entities.Scheduling
{
    public class DoctorSchedule : BaseEntity
    {
        public List<DayOfWeek> DayOfWeeks { get; set; } = new List<DayOfWeek>();
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int MaxPatients { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidUntil { get; set; }
        public bool IsActive { get; set; } = true;
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
