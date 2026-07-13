using AutoMapper;
using HospitalManagement.Application.DTO.Identity.Role;
using HospitalManagement.Application.Features.Identity.Role.Commands.Create;
using HospitalManagement.Application.Features.Identity.Role.Commands.Update;
using HospitalManagement.Application.Features.Identity.Role.Queries.GetRolePermissions;
using HospitalManagement.Application.Features.Identity.Role.Queries.GetRoles;
using HospitalManagement.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Mappings.Identity
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role,RoleResponse>();
            CreateMap<CreateRoleCommand,Role>();
            CreateMap<UpdateRoleCommand,Role>();
            CreateMap<Role, RolePermissionsResponse>()
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.UserPermission));
            CreateMap<RolePermission, PermissionDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Permission.Id))
                .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.Permission.Token))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Permission.Description))
                .ForMember(dest => dest.Module, opt => opt.MapFrom(src => src.Permission.Module));
        }
    }
}
