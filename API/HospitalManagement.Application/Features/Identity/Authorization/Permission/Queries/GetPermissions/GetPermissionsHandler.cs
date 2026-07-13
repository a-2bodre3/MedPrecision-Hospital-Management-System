using AutoMapper;
using HospitalManagement.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using PermissionEntity = HospitalManagement.Domain.Entities.Identity.Permission;

namespace HospitalManagement.Application.Features.Identity.Authorization.Permission.Queries.GetPermissions
{
    public class GetPermissionsHandler : IRequestHandler<PermissionsQuery, List<PermissionsResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPermissionsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<PermissionsResponse>> Handle(PermissionsQuery request, CancellationToken cancellationToken)
        {
            var permissions = await _unitOfWork.Repository<PermissionEntity>().ListAllAsync(cancellationToken);
            return _mapper.Map<List<PermissionsResponse>>(permissions);
        }
    }
}
