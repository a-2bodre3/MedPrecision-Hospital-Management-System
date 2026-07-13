using HospitalManagement.Domain.Entities.Common;
using HospitalManagement.Domain.Entities.Structure;
using HospitalManagement.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using HospitalManagement.Infrastructure.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Infrastructure.DataAccess.DbInitializers
{
    public class BranchInitializer
    {
        public static async Task BranchSeed(AppDbContext context)
        {
            if (await context.Branches.AnyAsync()) return;
            var address = new List<Address>
        {
            new Address()
            {
                Street = "Qasr El Aini", City = "Cairo", Country = "Egypt", AddressType = AddressType.BranchLocation
            },
            new Address()
            {
                Street = "Saad Zaghloul", City = "Alexandria", Country = "Egypt", AddressType = AddressType.BranchLocation
            },
        };
            await context.Addresses.AddRangeAsync(address);
            await context.SaveChangesAsync();

            var branches = new List<Branch>
        {
            new Branch
            {
                Name = "Cairo Branch",
                Code = "CAI-001",
                AddressId = address[0].Id,
                PhoneNumber = "01123456789",
                Address = null!,
                UserBranches = null!
            },
            new Branch
            {
                Name = "Alex Branch",
                Code = "ALX-001",
                AddressId = address[1].Id,
                PhoneNumber = "01234567890",
                Address = null!,
                UserBranches = null!
            },
        };
            await context.Branches.AddRangeAsync(branches);
            await context.SaveChangesAsync();
        }
    }
}
