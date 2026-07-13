using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Entities.Staff
{
    public class Specialization
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<SubSpecialty> SubSpecialties { get; set; } = new List<SubSpecialty>();
    }
}
