using AutoMapper;
using HospitalManagement.Domain.Exceptions;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Specifications.Roles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using RoleEntity = HospitalManagement.Domain.Entities.Identity.Role;

namespace HospitalManagement.Application.Features.Identity.Role.Queries.GetRolePermissions
{
    public class GetRolePermissionsHandler : IRequestHandler<RolePermissionsQuery, RolePermissionsResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRolePermissionsHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }



        public async Task<RolePermissionsResponse> Handle(RolePermissionsQuery request, CancellationToken cancellationToken)
        {
            var spec = new RoleWithPermissionsSpec(request.RoleId);
            var role = await _unitOfWork.Repository<RoleEntity>().GetEntityWithSpecAsync(spec , cancellationToken);
            if(role == null)
            {
                throw new NotFoundException("Role not found");
            }
            return _mapper.Map<RolePermissionsResponse>(role);
        }
    }
}
