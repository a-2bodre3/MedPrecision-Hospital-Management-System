using HospitalManagement.Domain.Exceptions;
using HospitalManagement.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using RoleEntity = HospitalManagement.Domain.Entities.Identity.Role;

namespace HospitalManagement.Application.Features.Identity.Role.Commands.Delete
{
    public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRoleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _unitOfWork.Repository<RoleEntity>().GetByIdAsync(request.Id , cancellationToken);
            if (role == null)
            {
                throw new NotFoundException("هذه الدور غير موجوده ");
            }

            _unitOfWork.Repository<RoleEntity>().Delete(role);
            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
