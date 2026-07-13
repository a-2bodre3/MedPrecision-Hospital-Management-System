using HospitalManagement.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Room.Commands.Create
{
    public record CreateRoomCommand : IRequest<bool>
    {
        public required string RoomNumber { get; set; }
        public required int Floor { get; set; }
        public required RoomType RoomType { get; set; }
        public required int Capacity { get; set; }
        public required int DepartmentId { get; set; }
        public required int BranchId { get; set; }
    }
}
