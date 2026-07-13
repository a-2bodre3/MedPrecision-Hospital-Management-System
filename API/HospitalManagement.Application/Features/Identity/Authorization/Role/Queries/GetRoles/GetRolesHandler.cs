using AutoMapper;
using HospitalManagement.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using RoleEntity =  HospitalManagement.Domain.Entities.Identity.Role;

namespace HospitalManagement.Application.Features.Identity.Role.Queries.GetRoles
{
    public class GetRolesHandler : IRequestHandler<RolesQuery, List<RoleResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRolesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<RoleResponse>> Handle(RolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _unitOfWork.Repository<RoleEntity>().ListAllAsync(cancellationToken);
            return _mapper.Map<List<RoleResponse>>(roles);
        }
    }
}
