using AutoMapper;
using DepartmentEntity =  HospitalManagement.Domain.Entities.Structure.Department;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Specifications.Department;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Department.Queries.GetAll
{
    public class GetAllDepartmentHandler : IRequestHandler<DepartmentQuery, List<DepartmentResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllDepartmentHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        

        public async Task<List<DepartmentResponse>> Handle(DepartmentQuery request, CancellationToken cancellationToken)
        {
            var spec = new DepartmentWithBranches();
            var department = await _unitOfWork.Repository<DepartmentEntity>().ListAllAsync(spec);
            return _mapper.Map<List<DepartmentResponse>>(department);
        }
    }
}
