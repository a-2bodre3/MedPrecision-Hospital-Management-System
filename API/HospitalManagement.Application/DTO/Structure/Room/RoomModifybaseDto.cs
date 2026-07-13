using HospitalManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.DTO.Structure.Room
{
    public abstract record RoomModifybaseDto
    {
        public required string RoomNumber { get; set; }
        public required int Floor { get; set; }
        public required RoomType RoomType { get; set; }
        public required int DepartmentId { get; set; }
        public required int BranchId { get; set; }
        public required int Capacity { get; set; }
    }
}
