using HospitalManagement.Domain.Entities.Scheduling;
using System;
using System.Collections.Generic;
using System.Text;
using DoctorScheduleEntity = HospitalManagement.Domain.Entities.Scheduling.DoctorSchedule;

namespace HospitalManagement.Domain.Specifications.DoctorSchedule
{
    public class RoomScheduleOverlapSpec : BaseSpecification<DoctorScheduleEntity>
    {
        public RoomScheduleOverlapSpec(int roomId, TimeSpan startTime, TimeSpan endTime, List<DayOfWeek> days)
            : base(s => s.RoomId == roomId
                        && s.StartTime < endTime
                        && s.EndTime > startTime
                        && s.DayOfWeeks.Any(day => days.Contains(day)))
        {
            
        }
    }
}
