using HospitalManagement.Domain.Entities.Identity;
using HospitalManagement.Domain.Entities.Structure;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Interfaces.Authentication
{
    public interface ITokenService
    {
        string CreateToken(User user, Role role, IList<Permission> permissions, Branch branch);
    }
}
