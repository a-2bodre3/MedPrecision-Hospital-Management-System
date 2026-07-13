using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using DoctorScheduleEntity = HospitalManagement.Domain.Entities.Scheduling.DoctorSchedule;

namespace HospitalManagement.Application.Features.Scheduling.DoctorSchedule.Commands.Delete
{
    public class DeleteDoctorScheduleValidator : AbstractValidator<DoctorScheduleEntity>
    {
        public DeleteDoctorScheduleValidator()
        {
            RuleFor(d => d.Id)
                .GreaterThan(0).WithMessage("معرف الميعاد غير صحيح");
        }
    }
}
