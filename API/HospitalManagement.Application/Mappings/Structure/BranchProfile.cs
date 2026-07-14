using AutoMapper;
using HospitalManagement.Application.Features.Structure.Branch.Commands.Create;
using HospitalManagement.Application.Features.Structure.Branch.Commands.Update;
using HospitalManagement.Application.Features.Structure.Branch.Queries.GetAll;
using HospitalManagement.Application.Features.Structure.Branch.Queries.GetById;
using HospitalManagement.Domain.Entities.Structure;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Mappings.Structure
{
    internal class BranchProfile : Profile
    {
        public BranchProfile()
        {
            CreateMap<Branch, BranchResponse>();
            CreateMap<Branch, BranchDetailsResponse>();
            CreateMap<CreateBranchCommand, Branch>();
            CreateMap<UpdateBranchCommand, Branch>();

        }
    }
}
