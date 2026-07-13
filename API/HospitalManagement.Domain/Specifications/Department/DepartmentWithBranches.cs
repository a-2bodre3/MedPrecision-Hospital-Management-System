using DepartmentEntity =  HospitalManagement.Domain.Entities.Structure.Department;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Specifications.Department
{
    public class DepartmentWithBranches : BaseSpecification<DepartmentEntity>
    {
        public DepartmentWithBranches()
        {
            AddInclude(d => d.Branch);   
        }
    }
}
