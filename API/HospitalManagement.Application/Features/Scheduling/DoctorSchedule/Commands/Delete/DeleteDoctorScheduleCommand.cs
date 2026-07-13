using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Scheduling.DoctorSchedule.Commands.Delete
{
    public record DeleteDoctorScheduleCommand : IRequest<bool>
    {
        public required int Id { get; set; }
    }
}
