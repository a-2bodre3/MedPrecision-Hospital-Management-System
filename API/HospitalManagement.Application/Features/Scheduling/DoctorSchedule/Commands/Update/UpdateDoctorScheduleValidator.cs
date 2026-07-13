using FluentValidation;
using HospitalManagement.Application.Features.Sheduling.DoctorSchedule.Commands.Create;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Scheduling.DoctorSchedule.Commands.Update
{
    public class UpdateDoctorScheduleValidator : AbstractValidator<UpdateDoctorScheduleCommand>
    {
        public UpdateDoctorScheduleValidator()
        {
            RuleFor(d => d.Id)
                .GreaterThan(0).WithMessage("معرف القسم غير صحيح");

            Include(new CreateDoctorScheduleValidator());
        }
    }
}
