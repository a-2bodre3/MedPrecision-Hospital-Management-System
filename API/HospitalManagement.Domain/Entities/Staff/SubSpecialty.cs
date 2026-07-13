using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Entities.Staff
{
    public class SubSpecialty
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
    }
}
