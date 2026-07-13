using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Identity.Role.Commands.Update
{
    public record UpdateRoleCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
