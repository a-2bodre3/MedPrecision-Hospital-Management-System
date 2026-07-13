using AutoMapper;
using HospitalManagement.Application.DTO.Common;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Specifications.DoctorSchedule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using DoctorScheduleEntity = HospitalManagement.Domain.Entities.Scheduling.DoctorSchedule;

namespace HospitalManagement.Application.Features.Scheduling.DoctorSchedule.Queries.GetAllDoctorSchedules
{
    public class GetAllDoctorSchedulesHandler : IRequestHandler<DoctorSchedulesQuery, PagedResult<DoctorSchedulesResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllDoctorSchedulesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResult<DoctorSchedulesResponse>> Handle(DoctorSchedulesQuery request, CancellationToken cancellationToken)
        {
            var spec = new DoctorScheduleFilterSpec(request.SpecializationId , request.PageNumber , request.PageSize);
            var doctorSchedules = await _unitOfWork.Repository<DoctorScheduleEntity>().ListAllAsync(spec,cancellationToken);

            var countSpec = new DoctorScheduleFilterSpec(request.SpecializationId);
            var totalItems = await _unitOfWork.Repository<DoctorScheduleEntity>().CountAsync(countSpec,cancellationToken);
            
            var doctorSchedulesResponse = _mapper.Map<List<DoctorSchedulesResponse>>(doctorSchedules);
           
            return new PagedResult<DoctorSchedulesResponse>
            {
                Items = doctorSchedulesResponse,
                TotalCount = totalItems,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
