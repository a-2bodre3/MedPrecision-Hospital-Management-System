using HospitalManagement.Domain.Enums;
using HospitalManagement.Domain.Value_Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Branch.Queries.GetAll
{
    public record BranchResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public bool IsActive { get; set; }
    }
}
