using HospitalManagement.Application.DTO.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Lookups.Queries.GetAppLookups
{
    public record LookupDto(int Id ,string Name);

    public record AppLookupsResponse
    {
        public List<LookupDto> Branches { get; set; } = new();
        public List<LookupDto> Departments { get; set; } = new();
        public List<LookupDto> Rooms { get; set; } = new();
        public List<LookupDto> Roles { get; set; } = new();
    }
}
