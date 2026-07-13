using FluentValidation;
using HospitalManagement.Application.DTO.Scheduling.Appointment;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Validators.Scheduling.Appointment
{
    internal class AppointmentCreateDtoValidator : AbstractValidator<AppointmentCreateDto>
    {
        public AppointmentCreateDtoValidator()
        {
            Include(new AppointmentBaseDtoValidator());

            RuleFor(a => a.DoctorScheduleId)
                .NotEmpty().WithMessage("يجب اختيار جدول الطبيب");

            RuleFor(a => a.PatientId)
                .NotEmpty().WithMessage("يجب اختيار المريض");
        }
    }
}
