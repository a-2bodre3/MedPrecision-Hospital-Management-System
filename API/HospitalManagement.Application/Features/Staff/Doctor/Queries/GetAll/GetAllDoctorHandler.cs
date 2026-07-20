using AutoMapper;
using HospitalManagement.Application.DTO.Common;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Specifications.Doctor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using DoctorEntity = HospitalManagement.Domain.Entities.Staff.Doctor;

namespace HospitalManagement.Application.Features.Staff.Doctor.Queries.GetAll
{
    public class GetAllDoctorHandler : IRequestHandler<DoctorsQuery, PagedResult<DoctorsResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllDoctorHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResult<DoctorsResponse>> Handle(DoctorsQuery request, CancellationToken cancellationToken)
        {
            var spec = new DoctorFilterSpec(request.SearchTerm, request.DepartmentId, request.IsActive, request.PageNumber ,request.PageSize);
            var countSpec = new DoctorFilterSpec(request.SearchTerm, request.DepartmentId, request.IsActive, 1, int.MaxValue);

            var doctors = await _unitOfWork.Repository<DoctorEntity>().ListAllAsync(spec, cancellationToken);
            var totalCount = await _unitOfWork.Repository<DoctorEntity>().CountAsync(countSpec, cancellationToken);

            var doctorsResponses = _mapper.Map<List<DoctorsResponse>>(doctors);

            return new PagedResult<DoctorsResponse>
            {
                Items = doctorsResponses,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
