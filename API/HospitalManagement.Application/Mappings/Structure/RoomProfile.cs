using AutoMapper;
using HospitalManagement.Application.Features.Structure.Room.Commands.Create;
using HospitalManagement.Application.Features.Structure.Room.Commands.Update;
using HospitalManagement.Application.Features.Structure.Room.Queries.GetAll;
using HospitalManagement.Application.Features.Structure.Room.Queries.GetById;
using HospitalManagement.Domain.Entities.Structure;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Mappings.Structure
{
    internal class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomResponse>();
            CreateMap<Room, RoomDetailsResponse>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src =>
                src.Department != null ? src.Department.Name : string.Empty))
            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src =>
                src.Branch != null ? src.Branch.Name : string.Empty));

            CreateMap <CreateRoomCommand, Room>();
            CreateMap<UpdateRoomCommand, Room>();
        }
    }
}
