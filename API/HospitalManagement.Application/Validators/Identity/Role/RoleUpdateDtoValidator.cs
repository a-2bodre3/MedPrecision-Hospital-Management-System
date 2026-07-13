using FluentValidation;
using HospitalManagement.Application.DTO.Identity.Role;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Validators.Identity.Role
{
    internal class RoleUpdateDtoValidator : AbstractValidator<RoleModifyDto>
    {
        public RoleUpdateDtoValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("اسم الدور مطلوب")
                .MaximumLength(100).WithMessage("اسم الدور لا يمكن أن يتجاوز 100 حرف");

        }
    }
}
