using FluentValidation;
using HospitalManagement.Application.DTO.Scheduling.Appointment;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Validators.Scheduling.Appointment
{
    internal class AppointmentStatusUpdateDtoValidator : AbstractValidator<AppointmentStatusUpdateDto>
    {
        public AppointmentStatusUpdateDtoValidator()
        {
            RuleFor(a => a.Status)
                .NotEmpty().WithMessage("يجب اختيار حالة الموعد");
        }
    }
}
