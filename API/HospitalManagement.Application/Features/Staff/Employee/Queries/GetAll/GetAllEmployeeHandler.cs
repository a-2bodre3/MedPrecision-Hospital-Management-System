using AutoMapper;
using HospitalManagement.Application.DTO.Common;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Specifications.Employee;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using EmployeeEntity = HospitalManagement.Domain.Entities.Staff.Employee;

namespace HospitalManagement.Application.Features.Staff.Employee.Queries.GetAll
{
    public class GetAllEmployeeHandler : IRequestHandler<EmployeeQuery, PagedResult<EmployeeResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllEmployeeHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResult<EmployeeResponse>> Handle(EmployeeQuery request, CancellationToken cancellationToken)
        {
            var spec = new EmployeeFilterSpec(request.SearchTerm, request.DepartmentId, request.IsActive, request.PageNumber, request.PageSize);

            var countSpec = new EmployeeFilterSpec(request.SearchTerm, request.DepartmentId, request.IsActive, 1, int.MaxValue);

            var employees = await _unitOfWork.Repository<EmployeeEntity>().ListAllAsync(spec, cancellationToken);
            var totalCount = await _unitOfWork.Repository<EmployeeEntity>().CountAsync(countSpec);


            var employeeResponses = _mapper.Map<List<EmployeeResponse>>(employees);

            return new PagedResult<EmployeeResponse> 
            {
                Items = employeeResponses,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
