using HospitalManagement.Domain.Entities.Identity;
using HospitalManagement.Infrastructure.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Infrastructure.DataAccess.DbInitializers
{
    public class RoleInitializer
    {
        public static async Task SeedRoles(AppDbContext context)
        {
            if (await context.Roles.AnyAsync()) return;

            var roles = new List<Role>
        {
            new Role() { Name = "Super Admin" },
            new Role() { Name = "Admin" },
            new Role() { Name = "Doctor" },
            new Role() { Name = "Nurse" },
            new Role() { Name = "Receptionist" },
            new Role() { Name = "Accountant" },
            new Role() { Name = "Pharmacist" },
            new Role() { Name = "Lab Technician" },
            new Role() { Name = "Patient" },
        };
            await context.Roles.AddRangeAsync(roles);
            await context.SaveChangesAsync();

            var allPermissions = await context.Permissions.ToListAsync();
            //----------------super admin permission----------------------
            var superAdmin = await context.Roles.FirstOrDefaultAsync(x => x.Name == "Super Admin");
            if (superAdmin == null)
            {
                throw new Exception("Role 'Super Admin' not found. Please ensure it is seeded.");
            }
            var superAdminPermissions = allPermissions
                .Where(p => p.Module != "Clinical & Consultation")
                .Select(p => new RolePermission
                {
                    RoleId = superAdmin.Id,
                    PermissionId = p.Id,
                    Permission = null!,
                    Role = null!
                });
            await context.RolePermissions.AddRangeAsync(superAdminPermissions);
            await context.SaveChangesAsync();

            //--------------admin permission----------------------
            var admin = await context.Roles.FirstOrDefaultAsync(x => x.Name == "Admin");
            if (admin == null)
            {
                throw new Exception("Role 'Admin' not found. Please ensure it is seeded.");
            }

            var adminPermissionsList = new[] { "User_Create", "User_Read", "User_Update", "Clinic_Setup", "Financial_Report_View", "Patient_Read_Basic" };
            var adminPermissions = allPermissions
            .Where(p => adminPermissionsList.Contains(p.Token))
            .Select(p => new RolePermission
            {
                RoleId = admin.Id,
                PermissionId = p.Id,
                Permission = null!,
                Role = null!
            });
            await context.RolePermissions.AddRangeAsync(adminPermissions);
            await context.SaveChangesAsync();

            //---------------doctor permission ---------------------------
            var doctor = await context.Roles.FirstOrDefaultAsync(x => x.Name == "Doctor");
            if (doctor == null)
            {
                throw new Exception("Role 'Doctor' not found. Please ensure it is seeded.");
            }

            var doctorPermissionsList = new[]
            {
            "Patient_Read_Basic", "Patient_Read_Medical", "Patient_Update", "Appointment_Read", "Consultation_Start",
            "Prescription_Create", "Diagnosis_Add", "Lab_Request_Create", "Lab_Request_Read", "Medical_Report_Generate"
        };
            var doctorPermissions = allPermissions
                .Where(p => doctorPermissionsList.Contains(p.Token))
                .Select(p => new RolePermission
                {
                    RoleId = doctor.Id,
                    PermissionId = p.Id,
                    Permission = null!,
                    Role = null!
                });
            await context.RolePermissions.AddRangeAsync(doctorPermissions);
            await context.SaveChangesAsync();

            //------------------Nurse Permission -------------------------
            var nurse = await context.Roles.FirstOrDefaultAsync(x => x.Name == "Nurse");
            if (nurse == null)
            {
                throw new Exception("Role 'Nurse' not found. Please ensure it is seeded.");
            }

            var nursePermissionsList = new[]
            {
            "Patient_Read_Basic", "Patient_Read_Medical", "Appointment_Read", "Vitals_Record",
            "Pharmacy_Prescription_Read"
        };
            var nursePermissions = allPermissions
                .Where(p => nursePermissionsList.Contains(p.Token))
                .Select(p => new RolePermission
                {
                    RoleId = nurse.Id,
                    PermissionId = p.Id,
                    Permission = null!,
                    Role = null!
                });
            await context.RolePermissions.AddRangeAsync(nursePermissions);
            await context.SaveChangesAsync();

            //------------------Receptionist Permission -------------------------
            var receptionist = await context.Roles.FirstOrDefaultAsync(x => x.Name == "Receptionist");
            if (receptionist == null)
            {
                throw new Exception("Role 'Receptionist' not found. Please ensure it is seeded.");
            }

            var receptionistPermissionsList = new[]
            {
            "Patient_Create", "Patient_Read_Basic", "Patient_Update", "Appointment_Create", "Appointment_Read",
            "Appointment_Update", "Appointment_Cancel", "Appointment_CheckIn", "Invoice_Create", "Invoice_Read"
        };
            var receptionistPermissions = allPermissions
                .Where(p => receptionistPermissionsList.Contains(p.Token))
                .Select(p => new RolePermission
                {
                    RoleId = receptionist.Id,
                    PermissionId = p.Id,
                    Permission = null!,
                    Role = null!
                });
            await context.RolePermissions.AddRangeAsync(receptionistPermissions);
            await context.SaveChangesAsync();

            //-----------------Accountant permission------------------------------
            var accountant = await context.Roles.FirstOrDefaultAsync(x => x.Name == "Accountant");
            if (accountant == null)
            {
                throw new Exception("Role 'Accountant' not found. Please ensure it is seeded.");
            }

            var accountantPermissionsList = new[]
            {
            "Inventory_Read", "Patient_Read_Basic"
        };

            var accountantPermissions = allPermissions
                .Where(p => accountantPermissionsList.Contains(p.Token) || p.Module == "Billing & Finance")
                .Select(p => new RolePermission
                {
                    RoleId = accountant.Id,
                    PermissionId = p.Id,
                    Permission = null!,
                    Role = null!
                });
            await context.RolePermissions.AddRangeAsync(accountantPermissions);
            await context.SaveChangesAsync();

            //-----------------pharmacist permission -----------------------------------------
            var pharmacist = await context.Roles.FirstOrDefaultAsync(x => x.Name == "Pharmacist");
            if (pharmacist == null)
            {
                throw new Exception("Role 'Pharmacist' not found. Please ensure it is seeded.");
            }

            var pharmacistPermissions = allPermissions
                .Where(p => p.Module == "Pharmacy & Inventory" || p.Token == "Patient_Read_Basic")
                .Select(p => new RolePermission
                {
                    RoleId = pharmacist.Id,
                    PermissionId = p.Id,
                    Permission = null!,
                    Role = null!
                });
            await context.RolePermissions.AddRangeAsync(pharmacistPermissions);
            await context.SaveChangesAsync();

            //--------------- lab technician permission -------------------------------------------------
            var labTech = await context.Roles.FirstOrDefaultAsync(x => x.Name == "Lab Technician");
            if (labTech == null)
            {
                throw new Exception("Role 'Lab Technician' not found. Please ensure it is seeded.");
            }
            var labTechPermissions = allPermissions
                .Where(p => p.Module == "Laboratory & Radiology" && p.Token != "Lab_Request_Create" || p.Token == " Patient_Read_Basic")
                .Select(p => new RolePermission
                {
                    RoleId = labTech.Id,
                    PermissionId = p.Id,
                    Permission = null!,
                    Role = null!
                });
            await context.RolePermissions.AddRangeAsync(labTechPermissions);
            await context.SaveChangesAsync();
            //------------------------------patient permission----------------------------------------
            var patient = await context.Roles.FirstOrDefaultAsync(x => x.Name == "Patient");
            if (patient == null)
            {
                throw new Exception("Role 'Patient' not found. Please ensure it is seeded.");
            }

            var patientPermissionsList = new[]
                { "Patient_Read_Basic", "Patient_Read_Medical", "Appointment_Create", "Appointment_Read", "Invoice_Read" };
            var patientPermissions = allPermissions
                .Where(p => patientPermissionsList.Contains(p.Token))
                .Select(p => new RolePermission
                {
                    RoleId = patient.Id,
                    PermissionId = p.Id,
                    Permission = null!,
                    Role = null!
                });
            await context.RolePermissions.AddRangeAsync(patientPermissions);
            await context.SaveChangesAsync();

        }
    }
}
