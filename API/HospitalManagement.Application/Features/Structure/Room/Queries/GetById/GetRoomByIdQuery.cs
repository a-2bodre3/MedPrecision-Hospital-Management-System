using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Room.Queries.GetById
{
    public record RoomDetailsQuery : IRequest<RoomDetailsResponse>
    {
        public int Id { get; set; }
    }
}