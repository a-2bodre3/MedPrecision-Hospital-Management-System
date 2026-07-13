using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Value_Objects
{
    public record Address
    {
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
    }
}
