using FluentValidation;
using HospitalManagement.Application.Validators.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Patient.Commands.Update
{
    public class UpdatePatientValidator : AbstractValidator<UpdatePatientCommand>
    {
        public UpdatePatientValidator()
        {
            RuleFor(e => e.FirstName)

                .NotEmpty().WithMessage("الاسم الأول مطلوب.")
                .MaximumLength(50).WithMessage("الاسم الأول لا يجب أن يتخطى 50 حرف.");

            RuleFor(e => e.LastName)
                .NotEmpty().WithMessage("الاسم العائلة مطلوب.")
                .MaximumLength(50).WithMessage("الاسم العائلة لا يجب أن يتخطى 50 حرف.");

            RuleFor(e => e.PhoneNumber)
                  .NotEmpty().WithMessage("رقم الهاتف مطلوب.");
            RuleFor(e => e.IsActive)
                .NotNull().WithMessage("حالة الموظف مطلوبة");
  

            RuleFor(e => e.Gender)
                .NotEmpty().WithMessage("تحديد النوع مطلوب.");
            RuleFor(e => e.Address)
                .SetValidator(new AddressValidator());

        }
    }
}
