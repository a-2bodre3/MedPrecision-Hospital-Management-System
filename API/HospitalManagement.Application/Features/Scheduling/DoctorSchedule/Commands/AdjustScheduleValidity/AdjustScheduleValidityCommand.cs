using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Scheduling.DoctorSchedule.Commands.AdjustScheduleValidity
{
    public record AdjustScheduleValidityCommand : IRequest<bool>
    {
        public int DoctorScheduleId { get; set; }
        public required DateTime ValidFrom { get; set; }
        public  DateTime? ValidUntil { get; set; }
    }
}
