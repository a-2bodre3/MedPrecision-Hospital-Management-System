using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Room.Commands.Deletee
{
    public record DeleteRoomCommand : IRequest<bool>
    {
        public required int Id { get; set; }
    }
}
