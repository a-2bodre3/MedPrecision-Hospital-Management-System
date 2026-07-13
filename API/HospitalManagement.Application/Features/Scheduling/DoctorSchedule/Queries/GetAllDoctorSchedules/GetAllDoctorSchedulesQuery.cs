using HospitalManagement.Application.DTO.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Scheduling.DoctorSchedule.Queries.GetAllDoctorSchedules
{
    public record DoctorSchedulesQuery : QueryParameters , IRequest<PagedResult<DoctorSchedulesResponse>>
    {
        public int? SpecializationId { get; set; }
    }
}
