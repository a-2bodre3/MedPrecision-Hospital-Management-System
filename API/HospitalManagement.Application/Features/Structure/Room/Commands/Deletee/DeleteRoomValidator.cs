using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Room.Commands.Deletee
{
    public class DeleteRoomValidator : AbstractValidator<DeleteRoomCommand>
    {
        public DeleteRoomValidator()
        {
            RuleFor(r => r.Id)
                .GreaterThan(0).WithMessage("معرف الغرفه غير صحيح");
        }
    }
}
