using FluentValidation;
using HospitalManagement.Application.Features.Staff.Employee.Commands.Create;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Staff.Doctor.Commands.Create
{
    public class CreateDoctorValidator : AbstractValidator<CreateDoctorCommand>
    {
        public CreateDoctorValidator()
        {
            RuleFor(d => d.LicenseNumber)
                .NotEmpty().WithMessage("رقم الترخيص مطلوب");
            RuleFor(d => d.ConsultationFee)
                .GreaterThan(0).WithMessage("رسوم الاستشارة يجب أن تكون أكبر من الصفر");
            RuleFor(d => d.Degree)
                .IsInEnum().WithMessage("الدرجة العلمية غير صالحة");
            RuleFor(d => d.SubSpecialtyId)
                .GreaterThan(0).WithMessage("التخصص الفرعي مطلوب");
            Include(new CreateEmployeeValidator());
        }
    }
}
