using HospitalManagement.Domain.Entities.Patients;
using System;
using System.Collections.Generic;
using System.Text;
using PatientEntity = HospitalManagement.Domain.Entities.Patients.Patient;

namespace HospitalManagement.Domain.Specifications.Patient
{
    public class PatientDetailsSpec : BaseSpecification<PatientEntity>
    {
        public PatientDetailsSpec(int patientId) : base(p => p.Id == patientId)
        {
            AddInclude(p => p.User);
            AddInclude(p => p.Address);
            AddInclude($"{nameof(PatientEntity.PatientAllergies)}.{nameof(PatientAllergy.Allergy)}");
            AddInclude($"{nameof(PatientEntity.PatientChronicDiseases)}.{nameof(PatientChronicDisease.ChronicDisease)}");
        }
    }
}
