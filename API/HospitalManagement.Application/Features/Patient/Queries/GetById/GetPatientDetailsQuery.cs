using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Patient.Queries.GetById
{
    public record PatientDetailsQuery : IRequest<PatientDetailsResponse>
    {
        public int PatientId { get; set; }
    }
}
