using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Department.Commands.Create
{
    public class CreateDepartmentValidator : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("اسم القسم مطلوب")
                .MinimumLength(3).WithMessage("اسم القسم لا يجب ان يقل عن 3 احرف");

            RuleFor(d => d.BranchId)
                .GreaterThan(0).WithMessage("ادخل فرع صالح");
                
        }
    }
}
