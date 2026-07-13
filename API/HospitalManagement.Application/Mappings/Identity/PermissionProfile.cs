using AutoMapper;
using HospitalManagement.Application.Features.Identity.Authorization.Permission.Queries.GetPermissions;
using HospitalManagement.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Mappings.Identity
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<Permission, PermissionsResponse>();
        }
    }
}
