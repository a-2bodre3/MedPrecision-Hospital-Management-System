using FluentValidation;
using HospitalManagement.Application.Features.Structure.Room.Commands.Create;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Room.Commands.Update
{
    public class UpdateRoomValidator : AbstractValidator<UpdateRoomCommand>
    {
        public UpdateRoomValidator()
        {
            Include(new CreateRoomValidator());

            RuleFor(r => r.IsActive)
                .NotEmpty().WithMessage("يجب ادخال حاله الغرقه ");
        }
    }
}
