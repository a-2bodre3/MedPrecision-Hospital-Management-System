using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Room.Commands.Create
{
    public class CreateRoomValidator : AbstractValidator<CreateRoomCommand>
    {
        public CreateRoomValidator()
        {
            RuleFor(r => r.RoomNumber)
                .NotEmpty().WithMessage("رفم الغرفه مطلوب");

            RuleFor(r => r.Floor)
                .GreaterThan(0).WithMessage("ادخل دور الغرفه صحيح");

            RuleFor(r => r.Capacity)
                .GreaterThan(0).WithMessage("الغرفه يجب علي الاقل ان تتسع لشخص واحد");

            RuleFor(r => r.DepartmentId)
                .GreaterThan(0).WithMessage("معرف القسم غير صحيح");

            RuleFor(r => r.BranchId)
                .GreaterThan(0).WithMessage("معرف الفرع غير صحيح");
        }
    }
}
