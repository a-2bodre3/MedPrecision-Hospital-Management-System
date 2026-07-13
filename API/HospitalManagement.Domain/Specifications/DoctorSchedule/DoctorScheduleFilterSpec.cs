using System;
using System.Collections.Generic;
using System.Text;
using DoctorScheduleEntity = HospitalManagement.Domain.Entities.Scheduling.DoctorSchedule;
using DoctorEntity = HospitalManagement.Domain.Entities.Staff.Doctor;
using EmployeeEntity = HospitalManagement.Domain.Entities.Staff.Employee;

namespace HospitalManagement.Domain.Specifications.DoctorSchedule
{
    public class DoctorScheduleFilterSpec : BaseSpecification<DoctorScheduleEntity>
    {
        public DoctorScheduleFilterSpec(int? specializationId, int pageNumber, int pageSize)
            : base(s => !specializationId.HasValue || s.Doctor.SubSpecialty.SpecializationId == specializationId)
        {
            AddInclude(s => s.Doctor);
            AddInclude(s => s.Room);

            AddInclude($"{nameof(DoctorScheduleEntity.Doctor)}.{nameof(DoctorEntity.SubSpecialty)}");
            AddInclude($"{nameof(DoctorScheduleEntity.Doctor)}.{nameof(DoctorEntity.Employee)}.{nameof(EmployeeEntity.User)}");

            AddOrderBy(s => s.StartTime);
            ApplyPaging((pageNumber - 1) * pageSize, pageSize);
        }
        public DoctorScheduleFilterSpec(int? specializationId)
            : base(s => !specializationId.HasValue || s.Doctor.SubSpecialty.SpecializationId == specializationId)
        {   }   
    }
}
