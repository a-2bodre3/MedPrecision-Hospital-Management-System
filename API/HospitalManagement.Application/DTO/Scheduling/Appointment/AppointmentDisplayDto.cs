using HospitalManagement.Domain.Enums;

namespace HospitalManagement.Application.DTO.Scheduling.Appointment;

public record AppointmentDisplayDto(
    int Id,
    DateTime AppointmentDate,
    DateTime CreatedAt,
    AppointmentType  AppointmentType,
    AppointmentsStatus   AppointmentsStatus,
    PaymentMethod  PaymentMethod,
    decimal Price,
    string? Notes,
    string PatientName,
    string DoctorName,
    string RoomNumber
    );