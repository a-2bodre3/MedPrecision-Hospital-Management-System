using HospitalManagement.Application.Features.Sheduling.DoctorSchedule.Commands.Create;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Scheduling.DoctorSchedule.Commands.Update
{
    public record UpdateDoctorScheduleCommand :CreateDoctorScheduleCommand ,IRequest<bool>
    {
        public int Id { get; set; }
    }
}
