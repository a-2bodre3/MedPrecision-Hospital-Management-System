using System;
using System.Collections.Generic;
using System.Text;
using DoctorScheduleEntity = HospitalManagement.Domain.Entities.Scheduling.DoctorSchedule;

namespace HospitalManagement.Domain.Specifications.DoctorSchedule
{
    public class DoctorScheduleOverlapSpec : BaseSpecification<DoctorScheduleEntity>
    {
        public DoctorScheduleOverlapSpec(int doctorId, TimeSpan startTime, TimeSpan endTime, List<DayOfWeek> days)
            : base(s => s.DoctorId == doctorId
                        && s.StartTime < endTime
                        && s.EndTime > startTime
                        && s.DayOfWeeks.Any(day => days.Contains(day)))
        {
            
        }
    }
}
