using HospitalManagement.Domain.Entities.Common;
using HospitalManagement.Domain.Entities.Identity;
using HospitalManagement.Domain.Entities.Scheduling;
using HospitalManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Entities.Patients
{
    public class Patient
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public required string PatientCode { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }

        public string? EmergencyPhoneNumber { get; set; }
        public string? EmergencyEmail { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public ICollection<PatientAllergy> PatientAllergies { get; set; } = new List<PatientAllergy>();
        public ICollection<PatientChronicDisease> PatientChronicDiseases { get; set; } = new List<PatientChronicDisease>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
