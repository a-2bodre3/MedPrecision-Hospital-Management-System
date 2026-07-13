using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Interfaces
{
    public interface ICurrentUserService
    {
        int? UserId { get; }
    }
}
