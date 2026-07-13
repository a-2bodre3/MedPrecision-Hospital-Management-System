using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.DTO.Common
{
    public record LookupsDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}