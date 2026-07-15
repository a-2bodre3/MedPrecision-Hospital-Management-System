using HospitalManagement.Application.DTO.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Staff.Doctor.Queries.GetAll
{
    public record DoctorsQuery : QueryParameters , IRequest<PagedResult<DoctorsResponse>>
    {
        public string? SearchTerm { get; init; }
        public bool? IsActive { get; init; }
        public int? DepartmentId { get; init; }
    }
}
