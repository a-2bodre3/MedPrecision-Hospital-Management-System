using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Department.Queries.GetAll
{
    public record DepartmentResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string BranchName { get; set; }
        public required string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
