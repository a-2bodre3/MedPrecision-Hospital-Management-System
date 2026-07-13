using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Room.Queries.GetAll
{
    public record RoomQuery : IRequest<List<RoomResponse>>;
}
