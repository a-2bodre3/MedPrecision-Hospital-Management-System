using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Scheduling.DoctorSchedule.Queries.GetAllDoctorSchedules
{
    public record DoctorSchedulesResponse
    {
        public int Id { get; set; }
        public List<DayOfWeek> DayOfWeeks { get; set; } = new List<DayOfWeek>();
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int MaxPatients { get; set; }
        public required string DoctorName {  get; set; }
        public required string RoomNumber {  get; set; }
    }
}
