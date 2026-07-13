using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Identity.Role.Commands.Delete
{
    public record DeleteRoleCommand : IRequest<bool>
    {
        public required int Id { get; set; }
    }
}
