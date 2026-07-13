using AutoMapper;
using HospitalManagement.Domain.Entities.Structure;
using HospitalManagement.Domain.Exceptions;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Specifications.Department;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using DepartmentEntity = HospitalManagement.Domain.Entities.Structure.Department;

namespace HospitalManagement.Application.Features.Structure.Department.Commands.Update
{
    public class UpdateDepartmentHandler : IRequestHandler<UpdateDepartmentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateDepartmentHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var spec = new DepartmentDetailsSpec(request.Id);
            var department = await _unitOfWork.Repository<DepartmentEntity>().GetEntityWithSpecAsync(spec);

            if (department == null)
            {
                throw new NotFoundException("هذا القسم غير موجود");
            }
            _mapper.Map(request, department);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;

        }
    }
}
