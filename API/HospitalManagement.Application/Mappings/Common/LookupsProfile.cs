using AutoMapper;
using HospitalManagement.Application.DTO.Common;
using HospitalManagement.Domain.Entities.Identity;
using HospitalManagement.Domain.Entities.Patients;
using HospitalManagement.Domain.Entities.Staff;
using HospitalManagement.Domain.Entities.Structure;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Mappings.Common
{
    internal class LookupsProfile : Profile
    {
        public LookupsProfile()
        {
            CreateMap<Department,LookupsDto>();
            CreateMap<Branch,LookupsDto>();
            CreateMap<Role,LookupsDto>();
            CreateMap<Allergy, LookupsDto>();
            CreateMap<ChronicDisease, LookupsDto>();
            CreateMap<SubSpecialty, LookupsDto>();
        }
    }
}
