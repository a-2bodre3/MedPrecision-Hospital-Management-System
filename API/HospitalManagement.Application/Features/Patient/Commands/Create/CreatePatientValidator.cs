using FluentValidation;
using HospitalManagement.Application.Validators.Common;
using HospitalManagement.Application.Validators.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Patient.Commands.Create
{
    public class CreatePatientValidator : AbstractValidator<CreatePatientCommand>
    {
        public CreatePatientValidator()
        {
            RuleFor(p => p.Email)
                .IsValidEmail();
            RuleFor(p => p.Password)
                .IsValidPassword();
            RuleFor(p => p.FirstName)

                .NotEmpty().WithMessage("الاسم الأول مطلوب.")
                .MaximumLength(50).WithMessage("الاسم الأول لا يجب أن يتخطى 50 حرف.");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("الاسم العائلة مطلوب.")
                .MaximumLength(50).WithMessage("الاسم العائلة لا يجب أن يتخطى 50 حرف.");

            RuleFor(p => p.PhoneNumber)
                  .NotEmpty().WithMessage("رقم الهاتف مطلوب.");
            RuleFor(p => p.DateOfBirth)
                .NotEmpty().WithMessage("تاريخ الميلاد مطلوب");

            RuleFor(p => p.Gender)
                .NotEmpty().WithMessage("تحديد النوع مطلوب.");
            RuleFor(p => p.Address)
                .SetValidator(new AddressValidator());
        }
    }
}
