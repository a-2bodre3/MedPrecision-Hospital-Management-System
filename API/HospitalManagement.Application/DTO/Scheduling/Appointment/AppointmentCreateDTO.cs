using HospitalManagement.Domain.Enums;

namespace HospitalManagement.Application.DTO.Scheduling.Appointment;

public record AppointmentCreateDto : AppointmentBaseDto
{
    public int DoctorScheduleId { get; set; }
    public int PatientId { get; set; }
}