using HospitalManagement.Domain.Entities.Scheduling;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Specifications.DoctorSchedule
{
    public class GetFutureAppointmentsByScheduleSpec : BaseSpecification<Appointment>
    {
        public GetFutureAppointmentsByScheduleSpec(int scheduleId, DateTime date)
            :base(a => a.DoctorScheduleId == scheduleId && a.AppointmentDate >= date)
        {
            
        }
    }
}
