using HospitalManagement.Domain.Exceptions;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Specifications.Employee;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using EmployeeEntity = HospitalManagement.Domain.Entities.Staff.Employee;

namespace HospitalManagement.Application.Features.Staff.Employee.Commands.Delete
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEmployeeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var spec = new EmployeeDetailsSpec(request.Id);
            var employee = await _unitOfWork.Repository<EmployeeEntity>().GetEntityWithSpecAsync(spec, cancellationToken);

            if (employee == null)
            {
                throw new NotFoundException($"Employee with Id {request.Id} not found.");
            }
            employee.User.IsActive = false;
            _unitOfWork.Repository<EmployeeEntity>().Update(employee);
            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
