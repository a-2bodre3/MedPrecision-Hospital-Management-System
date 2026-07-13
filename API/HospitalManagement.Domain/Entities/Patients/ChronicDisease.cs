using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Entities.Patients
{
    public class ChronicDisease
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<PatientChronicDisease> PatientChronicDiseases { get; set; } = new List<PatientChronicDisease>();
    }
}
