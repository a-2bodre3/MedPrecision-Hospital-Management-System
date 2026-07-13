using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Sheduling.DoctorSchedule.Commands.Create
{
    public record CreateDoctorScheduleCommand : IRequest<bool>
    {
        public required List<DayOfWeek> DayOfWeeks { get; set; }
        public required TimeSpan StartTime { get; set; }
        public required TimeSpan EndTime { get; set; }
        public required int MaxPatients { get; set; }
        public required DateTime ValidFrom { get; set; }
        public required int DoctorId { get; set; }
        public required int RoomId { get; set; }
    }
}
