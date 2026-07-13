using HospitalManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Room.Queries.GetAll
{
    public record RoomResponse
    {
        public int Id { get; set; }
        public required string RoomNumber { get; set; }
        public required RoomType RoomType { get; set; }
        public required bool IsActive { get; set; }
    }
}
