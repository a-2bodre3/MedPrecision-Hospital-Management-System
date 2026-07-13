using FluentValidation;
using HospitalManagement.Application.Features.Staff.Employee.Commands.Update;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Staff.Doctor.Commands.Update
{
    public class UpdateDoctorValidator : AbstractValidator<UpdateDoctorCommand>
    {
        public UpdateDoctorValidator()
        {

            RuleFor(d => d.ConsultationFee)
                .GreaterThan(0).WithMessage("رسوم الاستشارة يجب أن تكون أكبر من الصفر");

            Include(new UpdateEmployeeValidator());
        }
    }
}
