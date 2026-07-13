using System;
using System.Collections.Generic;
using System.Text;
using DoctorScheduleEntity = HospitalManagement.Domain.Entities.Scheduling.DoctorSchedule;

namespace HospitalManagement.Domain.Specifications.DoctorSchedule
{
    public class DoctorScheduleDetailsSpec : BaseSpecification<DoctorScheduleEntity>
    {
        public DoctorScheduleDetailsSpec(int doctorScheduleId)
            : base(s => s.Id == doctorScheduleId)
        {
            AddInclude(s => s.Doctor);
            AddInclude(s => s.Room);
        }
    }
}
