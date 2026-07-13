using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Department.Commands.Delete
{
    public class DeleteDepartmentValidator : AbstractValidator<DeleteDepartmentCommand>
    {
        public DeleteDepartmentValidator()
        {
            RuleFor(d => d.Id)
                .GreaterThan(0).WithMessage("معرف القسم غير صحيح");
        }
    }
}
