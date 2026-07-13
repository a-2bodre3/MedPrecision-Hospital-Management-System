using AutoMapper;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Specifications.Doctor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using DoctorEntity = HospitalManagement.Domain.Entities.Staff.Doctor;

namespace HospitalManagement.Application.Features.Staff.Doctor.Queries.GetDetails
{
    public class GetDoctorDetailsHandler : IRequestHandler<DoctorDetailsQuery, DoctorDetailsResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetDoctorDetailsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DoctorDetailsResponse> Handle(DoctorDetailsQuery request, CancellationToken cancellationToken)
        {
            var spec = new DoctorDetailsSpec(request.DoctorId);
            var doctor = await _unitOfWork.Repository<DoctorEntity>().GetEntityWithSpecAsync(spec, cancellationToken);
            return _mapper.Map<DoctorDetailsResponse>(doctor);
        }
    }
}
