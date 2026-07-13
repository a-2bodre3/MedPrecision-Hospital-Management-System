using FluentValidation;
using HospitalManagement.Application.DTO.Scheduling.DoctorSchedule;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Validators.Scheduling.DoctorSchedule
{
    public class DoctorScheduleFormDtoValidator : AbstractValidator<DoctorScheduleFormDto>
    {
        public DoctorScheduleFormDtoValidator()
        {
            RuleFor(s => s.DayOfWeeks)
                .NotEmpty().WithMessage("يجب ادخال يوم واحد علي الاقل");

            RuleFor(s => s.StartTime)
                .NotEmpty().WithMessage("يجب ادخال وقت البدء");

            RuleFor(s => s.EndTime)
                .NotEmpty().WithMessage("يجب ادخال وقت الانتهاء");

            RuleFor(s => s.MaxPatients)
                .NotEmpty().WithMessage("يجب ادخال الحد الاقصي للمرضى")
                .InclusiveBetween(1, 30).WithMessage("الحد الادني للمرضى يجب ان يكون اكبر من صفر والحد الاقصي 30 مريض");

            RuleFor(s => s.DoctorId)
                .NotEmpty().WithMessage("يجب اختيار طبيب");

            RuleFor(s => s.RoomId)
                .NotEmpty().WithMessage("يجب اختيار غرفة");
        }
    }
}
