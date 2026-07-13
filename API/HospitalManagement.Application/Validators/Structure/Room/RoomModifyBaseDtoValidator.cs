using FluentValidation;
using HospitalManagement.Application.DTO.Structure.Room;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Validators.Structure.Room
{
    public class RoomModifyBaseDtoValidator : AbstractValidator<RoomModifybaseDto>
    {
        public RoomModifyBaseDtoValidator()
        {
            RuleFor(r => r.RoomNumber)
                .NotEmpty().WithMessage("رقم الغرفه مطلوب")
                .MaximumLength(20).WithMessage("رقم الغرفه لا يمكن أن يتجاوز 20 حرف");

            RuleFor(r => r.Floor)
                .NotEmpty().WithMessage("رقم دور الغرفه مطلوب");

            RuleFor(r => r.RoomType)
                .NotEmpty().WithMessage("نوع الغرفه مطلوب");

            RuleFor(r => r.DepartmentId)
                .NotEmpty().WithMessage("القسم مطلوب");

            RuleFor(r => r.BranchId)
                .NotEmpty().WithMessage("الفرع مطلوب");

            RuleFor(r => r.Capacity)
                .NotEmpty().WithMessage("عدد الافراض المسموحه للغرفه مطلوب");
        }
    }
}
