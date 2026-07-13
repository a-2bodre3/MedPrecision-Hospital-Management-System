using AutoMapper;
using HospitalManagement.Application.Features.Structure.Department.Commands.Create;
using HospitalManagement.Application.Features.Structure.Department.Commands.Update;
using HospitalManagement.Application.Features.Structure.Department.Queries.GetAll;
using HospitalManagement.Domain.Entities.Structure;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Mappings.Structure
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentResponse>()
                .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src =>
                    src.Branch != null ? src.Branch.Name : string.Empty));

            CreateMap<CreateDepartmentCommand, Department>();
            CreateMap<UpdateDepartmentCommand, Department>();
        }
    }
}
