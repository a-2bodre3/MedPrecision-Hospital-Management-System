using FluentValidation;
using HospitalManagement.Application.DTO.Patients.Patient;
using HospitalManagement.Application.Validators.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Validators.Patients.Patient
{
    public class PatientModifyBasicDtoValidation : AbstractValidator<PatientModifyBasicDto>
    {
        public PatientModifyBasicDtoValidation()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("الاسم الأول مطلوب.")
                .MaximumLength(50).WithMessage("الاسم الأول لا يجب أن يتخطى 50 حرف.");

            RuleFor(p => p.LastName)
             .NotEmpty().WithMessage("الاسم العائلة مطلوب.")
             .MaximumLength(50).WithMessage("الاسم العائلة لا يجب أن يتخطى 50 حرف.");

            RuleFor(p => p.PhoneNumber)
                .NotEmpty().WithMessage("رقم الهاتف مطلوب.");

            RuleFor(p => p.ImageFile)
                .NotEmpty().WithMessage("صورة الموظف مطلوبة.");

            RuleFor(p => p.Gender)
                .NotEmpty().WithMessage("تحديد النوع مطلوب.");

            //RuleFor(p => p.Address)
            //    .SetValidator(new AddressValidator());
        }
    }
}
