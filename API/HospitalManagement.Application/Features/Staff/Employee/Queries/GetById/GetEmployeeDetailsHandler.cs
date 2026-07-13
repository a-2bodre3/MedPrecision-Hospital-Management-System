using AutoMapper;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Specifications.Employee;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using EmployeeEntity = HospitalManagement.Domain.Entities.Staff.Employee;

namespace HospitalManagement.Application.Features.Staff.Employee.Queries.GetById
{
    public class EmployeeDetailsHandler : IRequestHandler<EmployeeDetailsQuery, EmployeeDetailsResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeDetailsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<EmployeeDetailsResponse> Handle(EmployeeDetailsQuery request, CancellationToken cancellationToken)
        {
            var spec = new EmployeeDetailsSpec(request.EmployeeId);
            var employee = await _unitOfWork.Repository<EmployeeEntity>().GetEntityWithSpecAsync(spec, cancellationToken);
            
            return _mapper.Map<EmployeeDetailsResponse>(employee);
        }
    }
}
