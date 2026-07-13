using HospitalManagement.Application.Features.Structure.Room.Queries.GetAll;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Room.Queries.GetById
{
    public record RoomDetailsResponse : RoomResponse
    {
        public required int Floor { get; set; }
        public required int Capacity { get; set; }
        public required string DepartmentName { get; set; }
        public required string BranchName { get; set; }
    }
}
