using AutoMapper;
using HospitalManagement.Domain.Entities.Structure;
using HospitalManagement.Domain.Exceptions;
using HospitalManagement.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using DepartmentEntity = HospitalManagement.Domain.Entities.Structure.Department;

namespace HospitalManagement.Application.Features.Structure.Department.Commands.Delete
{
    public class DeleteDepartmentHandler : IRequestHandler<DeleteDepartmentCommand ,bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDepartmentHandler( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _unitOfWork.Repository<DepartmentEntity>().GetByIdAsync(request.Id);
            if (department == null)
            {
                throw new NotFoundException("هذا القسم غير موجود");
            }
          
            _unitOfWork.Repository<DepartmentEntity>().Delete(department);
            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
