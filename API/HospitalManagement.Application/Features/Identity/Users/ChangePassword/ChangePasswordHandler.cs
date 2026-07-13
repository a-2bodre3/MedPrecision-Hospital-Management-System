using HospitalManagement.Domain.Exceptions;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Interfaces.Authentication;
using HospitalManagement.Domain.Specifications.Employee;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using EmployeeEntity = HospitalManagement.Domain.Entities.Staff.Employee;

namespace HospitalManagement.Application.Features.Identity.Users.ChangePassword
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        public ChangePasswordHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var spec = new EmployeeDetailsSpec(request.Id);
            var employee = await _unitOfWork.Repository<EmployeeEntity>().GetEntityWithSpecAsync(spec, cancellationToken);

            if (employee == null)
            {
                throw new NotFoundException($"Employee with Id {request.Id} not found.");
            }
            employee.User.PasswordHash = _passwordHasher.HashPassword(request.Password);
            _unitOfWork.Repository<EmployeeEntity>().Update(employee);
            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
