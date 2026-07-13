using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Patient.Commands.Delete
{
    public record DeletePatientCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
