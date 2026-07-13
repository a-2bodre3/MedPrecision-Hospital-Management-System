using HospitalManagement.Application.Features.Structure.Room.Commands.Create;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Room.Commands.Update
{
    public record UpdateRoomCommand : CreateRoomCommand , IRequest<bool>
    {
        public int Id { get; set; }
        public required bool IsActive { get; set; }
    }
}
