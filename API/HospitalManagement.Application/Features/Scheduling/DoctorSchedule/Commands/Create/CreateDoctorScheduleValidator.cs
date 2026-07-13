using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Sheduling.DoctorSchedule.Commands.Create
{
    public class CreateDoctorScheduleValidator : AbstractValidator<CreateDoctorScheduleCommand>
    {
        public CreateDoctorScheduleValidator()
        {
            RuleFor(x => x.DayOfWeeks)
           .NotNull().WithMessage("يجب ادخال يوم واحد علي الاقل")
           .Must(x => x.Count > 0).WithMessage("يجب ادخال يوم واحد علي الاقل");

            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("يجب ادخال وقت البدء");

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("يجب ادخال وقت الانتهاء")
                .GreaterThan(x => x.StartTime)
                .WithMessage("وقت الانتهاء يجب ان يكون بعد وقت البدء");

            RuleFor(x => x.MaxPatients)
                .InclusiveBetween(1, 30)
                .WithMessage("الحد الادني للمرضى يجب ان يكون اكبر من صفر والحد الاقصي 30 مريض");

            RuleFor(x => x.ValidFrom)
                .NotEmpty().WithMessage("يجب ادخال تاريح البدء");

            RuleFor(x => x.DoctorId)
                .GreaterThan(0).WithMessage("يجب اختيار طبيب");

            RuleFor(x => x.RoomId)
                .GreaterThan(0).WithMessage("يجب اختيار غرفة");

        }
    }
}
