using AutoMapper;
using HospitalManagement.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using RoleEntity = HospitalManagement.Domain.Entities.Identity.Role;

namespace HospitalManagement.Application.Features.Identity.Role.Commands.Create
{
    public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateRoleHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = _mapper.Map<RoleEntity>(request);
            await _unitOfWork.Repository<RoleEntity>().AddAsync(role, cancellationToken);
            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        }
    }
}
