using AutoMapper;
using AddressEntity =  HospitalManagement.Domain.Entities.Common.Address;
using AddressValue = HospitalManagement.Domain.Value_Objects.Address;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Mappings.Common
{
    internal class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<AddressValue, AddressEntity>()
                .ForMember(dest => dest.AddressType , opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
