namespace HospitalManagement.Application.DTO.Scheduling.DoctorSchedule;

public record DoctorScheduleDisplayDto (
    int Id,
    List<DayOfWeek> DayOfWeeks,
    TimeSpan StartTime,
    TimeSpan EndTime,
    int MaxPatients,
    string DoctorName,
    string RoomNumber
    );