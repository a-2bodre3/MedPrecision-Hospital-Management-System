using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Entities.Patients
{
    public class PatientAllergy
    {
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int AllergyId { get; set; }
        public Allergy Allergy { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}
