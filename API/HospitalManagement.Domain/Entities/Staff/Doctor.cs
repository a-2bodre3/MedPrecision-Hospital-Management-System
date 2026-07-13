using HospitalManagement.Domain.Entities.Scheduling;
using HospitalManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Entities.Staff
{
    public class Doctor
    {
        public int Id { get; set; }

        public required string LicenseNumber { get; set; }
        public decimal ConsultationFee { get; set; }
        public AcademicDegree? Degree { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int SubSpecialtyId { get; set; }
        public SubSpecialty SubSpecialty { get; set; }

        public ICollection<DoctorSchedule> DoctorSchedules { get; set; } = new List<DoctorSchedule>();
    }
}
