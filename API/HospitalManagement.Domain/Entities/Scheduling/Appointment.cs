using HospitalManagement.Domain.Entities.Identity;
using HospitalManagement.Domain.Entities.Patients;
using HospitalManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Entities.Scheduling
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public AppointmentType Type { get; set; }
        public AppointmentsStatus Status { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal Price { get; set; }
        public string? Notes { get; set; }

        public int CreatedBy { get; set; }
        public User? CreatedByUser { get; set; }

        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

        public int DoctorScheduleId { get; set; }
        public DoctorSchedule? DoctorSchedule { get; set; }
    }
}
