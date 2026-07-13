using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Identity.Role.Commands.Delete
{
    public class DeleteRoleValidator : AbstractValidator<DeleteRoleCommand>
    {
        public DeleteRoleValidator()
        {
            RuleFor(r => r.Id)
                .GreaterThan(0).WithMessage("معرف الدور غير صالح");
        }
    }
}
