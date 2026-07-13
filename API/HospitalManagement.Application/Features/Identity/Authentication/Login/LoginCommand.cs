using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Identity.Authentication.Login
{

    public record LoginCommand : IRequest<LoginResponse>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    } 

    public record LoginResponse
    {
        public required string Token { get; set; }
    }

}
