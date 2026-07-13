using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.DTO.Common
{
    public record AddressDto
    {
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
    }
}
