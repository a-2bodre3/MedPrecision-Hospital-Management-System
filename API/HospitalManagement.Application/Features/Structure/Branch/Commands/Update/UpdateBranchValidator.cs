using FluentValidation;
using HospitalManagement.Application.Validators.Common;
using HospitalManagement.Domain.Value_Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Branch.Commands.Update
{
    public class UpdateBranchValidator : AbstractValidator<UpdateBranchCommand>
    {
        public UpdateBranchValidator()
        {
            RuleFor(b => b.Id)
                .GreaterThan(0).WithMessage("معرف الفرع غير صحيح");
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("اسم الفرع مطلوب");
            RuleFor(b => b.PhoneNumber)
                .NotEmpty().WithMessage("رقم الفرع مطلوب");
            RuleFor(b => b.Address)
                .SetValidator(new AddressValidator());
        }
    }
}