using HospitalManagement.Application.DTO.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Patient.Queries.GetAll
{
    public record PatientsQuery : QueryParameters, IRequest<PagedResult<PatientsResponse>>
    {
        public string? SearchTerm { get; init; }
        public bool? IsActive { get; init; }
    }
}
