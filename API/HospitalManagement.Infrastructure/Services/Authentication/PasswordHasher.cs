using HospitalManagement.Domain.Interfaces.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Infrastructure.Services.Authentication
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
            => BCrypt.Net.BCrypt.HashPassword(password);

        public bool VerifyPassword(string password, string hashedPassword)
            => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
