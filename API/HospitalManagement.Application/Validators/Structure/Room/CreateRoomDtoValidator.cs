using FluentValidation;
using HospitalManagement.Application.DTO.Structure.Room;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Validators.Structure.Room
{
    public class CreateRoomDtoValidator : AbstractValidator<CreateRoomDto>
    {
        public CreateRoomDtoValidator()
        {
            Include(new RoomModifyBaseDtoValidator());
        }
    }
}
