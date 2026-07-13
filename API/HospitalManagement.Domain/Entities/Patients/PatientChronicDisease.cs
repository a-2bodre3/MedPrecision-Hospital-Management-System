using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Entities.Patients
{
    public class PatientChronicDisease
    {
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int ChronicDiseaseId { get; set; }
        public ChronicDisease ChronicDisease { get; set; }

        public DateTime DiagnosedAt { get; set; } = DateTime.UtcNow;
    }
}
