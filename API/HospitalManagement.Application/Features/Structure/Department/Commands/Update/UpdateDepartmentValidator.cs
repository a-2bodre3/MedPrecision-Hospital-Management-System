using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Department.Commands.Update
{
    public class UpdateDepartmentValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("اسم القسم مطلوب");

            RuleFor(d => d.BranchId)
                .GreaterThan(0).WithMessage("يجب اختيار فرع للقسم");
        }
    }
}
