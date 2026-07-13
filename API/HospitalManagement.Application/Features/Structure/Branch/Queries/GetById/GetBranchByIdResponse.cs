using HospitalManagement.Application.Features.Structure.Branch.Queries.GetAll;
using HospitalManagement.Domain.Enums;
using HospitalManagement.Domain.Value_Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Branch.Queries.GetById
{
    public record BranchDetailsResponse : BranchResponse
    {
        public required string PhoneNumber { get; set; }
        public required AddressType AddressType { get; set; }

        public required Address Address { get; set; }
    }
}
