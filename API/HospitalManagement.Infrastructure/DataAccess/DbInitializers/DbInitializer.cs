using HospitalManagement.Domain.Interfaces.Authentication;
using HospitalManagement.Infrastructure.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Infrastructure.DataAccess.DbInitializers
{
    public class DbInitializer
    {
        public static async Task seedAllAsync(AppDbContext context, IPasswordHasher passwordHasher)
        {
            await PermissionInitializer.SeedPermissions(context);
            await RoleInitializer.SeedRoles(context);
            await BranchInitializer.BranchSeed(context);
            await AllergyInitializer.SeedAllergies(context);
            await ChronicDiseaseInitializer.SeedChronicDisease(context);
            await UserInitializer.seedUser(context, passwordHasher);
        }
    }
}
