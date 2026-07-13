using FluentValidation;
using HospitalManagement.Application.DTO.Patients.Patient;
using HospitalManagement.Application.Validators.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Validators.Patients.Patient
{
    internal class PatinetCreateDtoValidator : AbstractValidator<PatientCreateDto>
    {
        public PatinetCreateDtoValidator()
        {

            Include(new PatientModifyBasicDtoValidation());
            RuleFor(p => p.Email)
                .IsValidEmail();

            RuleFor(p => p.Password)
                .IsValidPassword();

            RuleFor(p => p.DateOfBirth)
                .NotEmpty().WithMessage("تاريخ الميلاد مطلوب.");

            RuleFor(p =>p.Gender)
                .NotEmpty().WithMessage("تحديد الجنس مطلوب.");
        }
    }
}