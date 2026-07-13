using HospitalManagement.Domain.Entities.Scheduling;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Specifications.DoctorSchedule
{
    public class GetFutureAppointmentsAfterDateSpec : BaseSpecification<Appointment>
    {
        public GetFutureAppointmentsAfterDateSpec(int scheduleId, DateTime targetDate)
            : base(a => a.DoctorScheduleId == scheduleId && a.AppointmentDate.Date > targetDate.Date)
        {
        }
    }
}
