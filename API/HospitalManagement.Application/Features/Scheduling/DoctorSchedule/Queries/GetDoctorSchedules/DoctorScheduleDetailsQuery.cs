using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Scheduling.DoctorSchedule.Queries.GetDoctorSchedules
{
    public record DoctorScheduleDetailsQuery : IRequest<DoctorScheduleDetailsResponse>
    {
        public int DoctorScheduleId { get; set; }
    }
}
