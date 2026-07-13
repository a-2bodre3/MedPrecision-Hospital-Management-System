using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Staff.Doctor.Queries.GetDetails
{
    public record DoctorDetailsQuery : IRequest<DoctorDetailsResponse>
    {
        public required int DoctorId { get; set; }
    }
}
