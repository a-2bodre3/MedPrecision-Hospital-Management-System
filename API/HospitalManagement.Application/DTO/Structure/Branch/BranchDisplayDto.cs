using HospitalManagement.Application.DTO.Common;
using HospitalManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.DTO.Structure.Branch
{
    public record BranchDisplayDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public required string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public required AddressType AddressType { get; set; }
        public required AddressDto Address { get; set; }
    }
}
