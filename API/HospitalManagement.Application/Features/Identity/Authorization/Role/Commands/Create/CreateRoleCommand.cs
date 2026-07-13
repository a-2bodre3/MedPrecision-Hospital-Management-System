using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Identity.Role.Commands.Create
{
    public record CreateRoleCommand : IRequest<bool>
    {
        public required string Name { get; set; }
    }
}
