using HospitalManagement.Domain.Entities.Structure;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Specifications.Branches
{
    public class BranchDetailsSpec : BaseSpecification<Branch>
    {
        public BranchDetailsSpec(int id): base(b => b.Id == id)
        {
            AddInclude(b => b.Address);
        }
    }
}
