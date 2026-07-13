using FluentValidation;
using HospitalManagement.Application.DTO.Structure.Branch;
using HospitalManagement.Application.Validators.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Validators.Structure.Branch
{
    internal class BranchModifyDtoValidator : AbstractValidator<BranchModifyDto>
    {
        public BranchModifyDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("اسم الفرع مطلوب");

            RuleFor(b => b.Code)
                .NotEmpty().WithMessage("كود الفرع مطلوب");

            RuleFor(b => b.PhoneNumber)
                .NotEmpty().WithMessage("رقم الفرع مطلوب");

            
        }
    }
}
