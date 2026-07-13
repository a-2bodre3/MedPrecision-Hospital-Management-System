using System;
using System.Collections.Generic;
using System.Text;
using RoomEntity = HospitalManagement.Domain.Entities.Structure.Room;

namespace HospitalManagement.Domain.Specifications.Room
{
    public class RoomDetailsSpec : BaseSpecification<RoomEntity>
    {
        public RoomDetailsSpec(int id) : base(r => r.Id == id)
        {
            AddInclude(r => r.Department);
            AddInclude(r => r.Branch);
        }
    }
}
