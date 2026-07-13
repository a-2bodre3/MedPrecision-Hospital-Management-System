using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Identity.Users.ChangePassword
{
    public record ChangePasswordCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public required string Password { get; set; }
    }
}
