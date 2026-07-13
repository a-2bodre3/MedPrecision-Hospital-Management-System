using AutoMapper;
using HospitalManagement.Application.DTO.Common;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Specifications.Patient;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using PatientEntity = HospitalManagement.Domain.Entities.Patients.Patient;

namespace HospitalManagement.Application.Features.Patient.Queries.GetAll
{
    public class GetAllPatientsHandler : IRequestHandler<PatientsQuery, PagedResult<PatientsResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllPatientsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResult<PatientsResponse>> Handle(PatientsQuery request, CancellationToken cancellationToken)
        {
            var spec = new PatientFilterSpec(request.SearchTerm, request.IsActive, request.PageNumber, request.PageSize);
            var countSpec = new PatientFilterSpec(request.SearchTerm,request.IsActive,1,int.MaxValue);

            var patients = await _unitOfWork.Repository<PatientEntity>().ListAllAsync(spec,cancellationToken);
            var totalCount = await _unitOfWork.Repository<PatientEntity>().CountAsync(countSpec, cancellationToken);

            var patientsResponse = _mapper.Map<List<PatientsResponse>>(patients);

            return new PagedResult<PatientsResponse>
            {
                Items = patientsResponse,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
