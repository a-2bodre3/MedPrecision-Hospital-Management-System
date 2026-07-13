using AutoMapper;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Specifications.Patient;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using PatientEntity = HospitalManagement.Domain.Entities.Patients.Patient;

namespace HospitalManagement.Application.Features.Patient.Queries.GetById
{
    public class GetPatientDetailsHandler : IRequestHandler<PatientDetailsQuery, PatientDetailsResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPatientDetailsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PatientDetailsResponse> Handle(PatientDetailsQuery request, CancellationToken cancellationToken)
        {
            var spec = new PatientDetailsSpec(request.PatientId);
            var patient = await _unitOfWork.Repository<PatientEntity>().GetEntityWithSpecAsync(spec, cancellationToken);
            return _mapper.Map<PatientDetailsResponse>(patient);
        }
    }
}
