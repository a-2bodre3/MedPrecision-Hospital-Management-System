using FluentValidation;
using HospitalManagement.Application.Validators.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Identity.Authentication.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(l => l.Email)
                .IsValidEmail();

            RuleFor(l => l.Password)
                .IsValidPassword();
        }
    }
}