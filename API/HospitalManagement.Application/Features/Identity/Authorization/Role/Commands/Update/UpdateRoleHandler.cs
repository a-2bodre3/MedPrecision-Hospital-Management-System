using AutoMapper;
using HospitalManagement.Domain.Exceptions;
using HospitalManagement.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using RoleEntity = HospitalManagement.Domain.Entities.Identity.Role;

namespace HospitalManagement.Application.Features.Identity.Role.Commands.Update
{
    public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateRoleHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _unitOfWork.Repository<RoleEntity>().GetByIdAsync(request.Id, cancellationToken);
            if (role == null)
                throw new NotFoundException("Role not found");

            _mapper.Map(request, role);
            _unitOfWork.Repository<RoleEntity>().Update(role);
            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
