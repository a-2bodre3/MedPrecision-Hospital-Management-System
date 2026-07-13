using HospitalManagement.Application.Features.Scheduling.DoctorSchedule.Queries.GetAllDoctorSchedules;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Scheduling.DoctorSchedule.Queries.GetDoctorSchedules
{
    public record DoctorScheduleDetailsResponse : DoctorSchedulesResponse
    {
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidUntil { get; set; }
        public bool IsActive { get; set; } 
        public DateTime CreatedAt { get; set; } 
        public required string CreatedByName { get; set; }
        public DateTime LastModifiedAt { get; set; }
        public required string LastModifiedByName { get; set; }

    }
}
