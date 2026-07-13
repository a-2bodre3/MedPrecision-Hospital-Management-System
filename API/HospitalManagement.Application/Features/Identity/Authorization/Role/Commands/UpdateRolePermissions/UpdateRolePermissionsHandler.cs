using AutoMapper;
using HospitalManagement.Domain.Entities.Identity;
using HospitalManagement.Domain.Exceptions;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Specifications.Roles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using RoleEntity = HospitalManagement.Domain.Entities.Identity.Role;

namespace HospitalManagement.Application.Features.Identity.Role.Commands.UpdateRolePermissions
{
    public class UpdateRolePermissionsHandler : IRequestHandler<UpdateRolePermissionsCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRolePermissionsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateRolePermissionsCommand request, CancellationToken cancellationToken)
        {
            var spec = new RoleWithPermissionsSpec(request.RoleId);
            var role = await _unitOfWork.Repository<RoleEntity>().GetEntityWithSpecAsync(spec , cancellationToken);
            if (role == null)
            {
                throw new NotFoundException("Role not found");
            }

            var existingPermissionIds = role.UserPermission.Select(p => p.PermissionId).ToList();
            var permissionsToRemove = existingPermissionIds.Except(request.PermissionIds).ToList();
            var permissionsToAdd = request.PermissionIds.Except(existingPermissionIds).ToList();

            if(permissionsToRemove.Any())
            {
                foreach(var permissionId in permissionsToRemove)
                {
                    var permission = role.UserPermission.FirstOrDefault(p => p.PermissionId == permissionId);
                    if (permission != null)
                    {
                        role.UserPermission.Remove(permission);
                    }
                }
            }
            if (permissionsToAdd.Any())
            {
                foreach (var permissionId in permissionsToAdd)
                {
                    var newPermission = new RolePermission
                    {
                        RoleId = role.Id,
                        PermissionId = permissionId
                    };
                    role.UserPermission.Add(newPermission);
                }
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
