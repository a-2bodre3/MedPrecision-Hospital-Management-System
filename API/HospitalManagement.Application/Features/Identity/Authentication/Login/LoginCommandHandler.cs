using HospitalManagement.Domain.Entities.Identity;
using HospitalManagement.Domain.Exceptions;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Interfaces.Authentication;
using HospitalManagement.Domain.Specifications.Roles;
using HospitalManagement.Domain.Specifications.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Identity.Authentication.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand ,LoginResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var spec = new UserByEmailWithDetailsSpec(request.Email);
            var user = await _unitOfWork.Repository<User>().GetEntityWithSpecAsync(spec);

            if(user == null || !_passwordHasher.VerifyPassword(request.Password , user.PasswordHash))
                throw new UnauthorizedException("البريد الالكتروني او كلمه السر غير صحيحه");

            if (!user.IsActive)
                throw new ForbiddenException("المستخدم ليس مفعل");

            var role = user.UserRoles.FirstOrDefault()?.Role;
            var branch = user.UserBranches.FirstOrDefault()?.Branch;

            if (role == null || branch == null)
                throw new BadRequestException("بيانات المستخدم غير مكتملة");

            var rolePermissions = await _unitOfWork.Repository<RolePermission>()
                .ListAllAsync(new RolePermissionsByRoleIdSpec(role.Id));

            var permissions = rolePermissions.Select(rp => rp.Permission).ToList();

            var token = _tokenService.CreateToken(user, role, permissions, branch);

            return new LoginResponse { Token = token };
        }
    }
}
