using FluentValidation;
using HospitalManagement.Application.Validators.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Branch.Commands.Create
{
    public class CreateBranchValidator : AbstractValidator<CreateBranchCommand>
    {
        public CreateBranchValidator() 
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("اسم الفرع مطلوب");

            RuleFor(b => b.Code)
                .NotEmpty().WithMessage("كود الفرع مطلوب");

            RuleFor(b => b.PhoneNumber)
                .NotEmpty().WithMessage("رقم الفرع مطلوب");
            RuleFor(b => b.Address)
                .SetValidator(new AddressValidator());
        }
    }
}
