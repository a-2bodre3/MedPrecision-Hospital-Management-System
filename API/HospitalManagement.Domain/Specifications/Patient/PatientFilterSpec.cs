using System;
using System.Collections.Generic;
using System.Text;
using PatientEntity = HospitalManagement.Domain.Entities.Patients.Patient;

namespace HospitalManagement.Domain.Specifications.Patient
{
    public class PatientFilterSpec : BaseSpecification<PatientEntity>
    {
        public PatientFilterSpec(string? searchTerm ,bool? isActive , int pageNumber , int pageSize)
            :base(p =>
            (string.IsNullOrEmpty(searchTerm) ||
            p.User.FirstName.ToLower().Contains(searchTerm.ToLower()) ||
            p.User.LastName.ToLower().Contains(searchTerm.ToLower()) ||
            p.PatientCode.ToLower().Contains(searchTerm.ToLower())) &&
            (!isActive.HasValue || p.User.IsActive == isActive.Value))
        {
            AddInclude(p => p.User);
        }
    }
}
