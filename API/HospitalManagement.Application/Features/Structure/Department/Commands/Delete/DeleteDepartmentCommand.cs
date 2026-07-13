using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Department.Commands.Delete
{
    public record DeleteDepartmentCommand : IRequest<bool>
    {
        public required int Id { get; set; }
    }
}
