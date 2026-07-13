using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Scheduling.DoctorSchedule.Commands.AdjustScheduleValidity
{
    public class AdjustScheduleValidityValidator : AbstractValidator<AdjustScheduleValidityCommand>
    {
        public AdjustScheduleValidityValidator()
        {
            RuleFor(d => d.DoctorScheduleId)
                .GreaterThan(0).WithMessage("معرف القسم غير صحيح");

            RuleFor(d => d.ValidFrom)
                .NotEmpty().WithMessage("ادخل تاريخ بدا صالح");
        }
    }
}
