using FluentValidation;
using HospitalManagement.Application.DTO.Structure.Room;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Validators.Structure.Room
{
    public class UpdateRoomDtoValidator : AbstractValidator<UpdateRoomDto>
    {
        public UpdateRoomDtoValidator()
        {
            Include(new RoomModifyBaseDtoValidator());

            RuleFor(r => r.IsActive)
                .NotEmpty().WithMessage("حاله الغرفه مطلوبه");
        }
    }
}
