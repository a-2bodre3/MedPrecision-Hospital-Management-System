using AutoMapper;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Specifications.DoctorSchedule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using DoctorScheduleEntity = HospitalManagement.Domain.Entities.Scheduling.DoctorSchedule;

namespace HospitalManagement.Application.Features.Scheduling.DoctorSchedule.Queries.GetDoctorSchedules
{
    public class DoctorScheduleDetailsHandler : IRequestHandler<DoctorScheduleDetailsQuery, DoctorScheduleDetailsResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DoctorScheduleDetailsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DoctorScheduleDetailsResponse> Handle(DoctorScheduleDetailsQuery request, CancellationToken cancellationToken)
        {
            var spec = new DoctorScheduleDetailsSpec(request.DoctorScheduleId);
            var DoctorSchedule = await _unitOfWork.Repository<DoctorScheduleEntity>().GetEntityWithSpecAsync(spec, cancellationToken);

            return _mapper.Map<DoctorScheduleDetailsResponse>(DoctorSchedule);
        }
    }
}
