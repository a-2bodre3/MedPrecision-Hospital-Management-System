using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Identity.Role.Commands.UpdateRolePermissions
{
    public class UpdateRolePermissionsValidator : AbstractValidator<UpdateRolePermissionsCommand>
    {
        public UpdateRolePermissionsValidator()
        {
            RuleFor(r => r.RoleId)
                .GreaterThan(0).WithMessage("معرف الدور غير صالح");

        }
    }
}
