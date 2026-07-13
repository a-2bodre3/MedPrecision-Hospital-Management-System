using FluentValidation;
using HospitalManagement.Application.DTO.Scheduling.Appointment;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Validators.Scheduling.Appointment
{
    public class AppointmentBaseDtoValidator : AbstractValidator<AppointmentBaseDto>
    {
        public AppointmentBaseDtoValidator()
        {
            RuleFor(a => a.AppointmentDate)
                .NotEmpty().WithMessage("يجب ادخال تاريخ الموعد");

            RuleFor(a => a.AppointmentType)
                .NotEmpty().WithMessage("يجب اختيار نوع الموعد");

            RuleFor(a => a.PaymentMethod)
                .NotEmpty().WithMessage("يجب اختيار طريقة الدفع");
            
        }
    }
}
