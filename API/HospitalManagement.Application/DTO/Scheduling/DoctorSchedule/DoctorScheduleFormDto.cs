namespace HospitalManagement.Application.DTO.Scheduling.DoctorSchedule;

public record DoctorScheduleFormDto(
    List<DayOfWeek> DayOfWeeks,
    TimeSpan StartTime,
    TimeSpan EndTime,
    int MaxPatients,
    int DoctorId,
    int RoomId
    );