using HospitalManagement.Domain.Entities.Structure;
using DepartmentEntity = HospitalManagement.Domain.Entities.Structure.Department;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Specifications.Department
{
    public class DepartmentDetailsSpec : BaseSpecification<DepartmentEntity>
    {
        public DepartmentDetailsSpec(int id) : base(d =>d.Id == id)
        {
        }
    }
}
