using AutoMapper;
using DepartmentEntity = HospitalManagement.Domain.Entities.Structure.Department;
using HospitalManagement.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Department.Commands.Create
{
    public class CreateDepartmentHandler : IRequestHandler<CreateDepartmentCommand , bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateDepartmentHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = _mapper.Map<DepartmentEntity>(request);
            department.IsActive = true;

            await _unitOfWork.Repository<DepartmentEntity>().AddAsync(department);
            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
