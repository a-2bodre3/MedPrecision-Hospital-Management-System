using FluentValidation;
using HospitalManagement.Application.DTO.Staff.Doctor;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Validators.Staff.Doctor
{
    public class CreateDoctorDtoValidator : AbstractValidator<CreateDoctorDto>
    {
        public CreateDoctorDtoValidator()
        {
            Include(new StaffInfoModifyCreateDtoValidator());

            RuleFor(d => d.LicenseNumber)
                .NotEmpty().WithMessage("رقم الرخصه الطبيه مطلوبة.");

            RuleFor(d => d.ConsultationFee)
                .NotEmpty().WithMessage("سعر الكشف مطلوبة.");

            RuleFor(d => d.Degree)
                .NotEmpty().WithMessage("الدرجه العليمه مطلوبه مطلوبة.");

            RuleFor(d => d.SubSpecialtyId)
                .NotEmpty().WithMessage("التخصص الطبي مطلوب مطلوبة.");
        }
    }
}
