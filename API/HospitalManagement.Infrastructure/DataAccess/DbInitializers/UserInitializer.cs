using HospitalManagement.Domain.Entities.Identity;
using HospitalManagement.Domain.Interfaces.Authentication;
using HospitalManagement.Infrastructure.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Infrastructure.DataAccess.DbInitializers
{
    public class UserInitializer
    {
        public static async Task seedUser(AppDbContext context, IPasswordHasher passwordHasher)
        {
            if (await context.Users.AnyAsync()) return;
            var users = new List<User>
        {
            new User()
            {
                Email = "superadmin@admin.com",
                PasswordHash = passwordHasher.HashPassword("Admin123!"),
                FirstName = "super",
                LastName = "admin",
                PhoneNumber = "01431354332",
            },
            new User()
            {
                Email = "superadmin1@admin.com",
                PasswordHash = passwordHasher.HashPassword("Admin123!"),
                FirstName = "super",
                LastName = "admin",
                PhoneNumber = "01436344332",
            },
            new User()
            {
                Email = "admin@admin.com",
                PasswordHash = passwordHasher.HashPassword("Admin123!"),
                FirstName = "admin",
                LastName = "admin",
                PhoneNumber = "01431044332",
            },
            new User()
            {
                Email = "doctor@admin.com",
                PasswordHash = passwordHasher.HashPassword("Admin123!"),
                FirstName = "doctor",
                LastName = "doctor",
                PhoneNumber = "01431364332",
            },
            new User()
            {
                Email = "nurse@admin.com",
                PasswordHash = passwordHasher.HashPassword("Admin123!"),
                FirstName = "nurse",
                LastName = "nurse",
                PhoneNumber = "01431744332",
            },
            new User()
            {
                Email = "receptionist@admin.com",
                PasswordHash = passwordHasher.HashPassword("Admin123!"),
                FirstName = "Receptionist",
                LastName = "Receptionist",
                PhoneNumber = "01431844332",
            },
            new User()
            {
                Email = "accountant@admin.com",
                PasswordHash = passwordHasher.HashPassword("Admin123!"),
                FirstName = "Accountant",
                LastName = "Accountant",
                PhoneNumber = "01431944332",
            },
            new User()
            {
                Email = "pharmacist@admin.com",
                PasswordHash = passwordHasher.HashPassword("Admin123!"),
                FirstName = "Pharmacist",
                LastName = "Pharmacist",
                PhoneNumber = "01431344532",
            },
            new User()
            {
                Email = "lab@admin.com",
                PasswordHash = passwordHasher.HashPassword("Admin123!"),
                FirstName = "Lab",
                LastName = "admin",
                PhoneNumber = "Technician",
            },
            new User()
            {
                Email = "patient@admin.com",
                PasswordHash = passwordHasher.HashPassword("Admin123!"),
                FirstName = "Patient",
                LastName = "Patient",
                PhoneNumber = "01431347332",
            },
        };
            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();

            var branches = context.Branches.ToList();
            //-------------------------------admin1 ---------------------------------------
            var admin1 = users.FirstOrDefault(u => u.Email == "superadmin@admin.com");
            if (admin1 == null)
            {
                throw new Exception("User not fount: ");
            }

            var admin1Branch = branches
                .Where(b => b.Name == "Cairo Branch")
                .Select(b => new UserBranch
                {
                    UserId = admin1.Id,
                    BranchId = b.Id,
                    User = null!,
                    Branch = null!
                }).FirstOrDefault();
            if (admin1Branch == null)
            {
                throw new Exception("User not fount: ");
            }
            await context.UserBranches.AddAsync(admin1Branch);
            await context.SaveChangesAsync();
            //-------------------------------admin2 ---------------------------------------
            var admin2 = users.FirstOrDefault(u => u.Email == "superadmin1@admin.com");
            if (admin2 == null)
            {
                throw new Exception("User not fount: ");
            }

            var admin2Branch = branches
                .Where(b => b.Name == "Alex Branch")
                .Select(b => new UserBranch
                {
                    UserId = admin2.Id,
                    BranchId = b.Id,
                    User = null!,
                    Branch = null!
                }).FirstOrDefault();
            if (admin2Branch == null)
            {
                throw new Exception("User not fount: ");
            }
            await context.UserBranches.AddAsync(admin2Branch);
            await context.SaveChangesAsync();

            //-------------------------------doctor ---------------------------------------
            var doctor = users.FirstOrDefault(u => u.Email == "doctor@admin.com");
            if (doctor == null)
            {
                throw new Exception("User not fount: ");
            }

            var doctorBranch = branches
                .Where(b => b.Name == "Cairo Branch")
                .Select(b => new UserBranch
                {
                    UserId = doctor.Id,
                    BranchId = b.Id,
                    User = null!,
                    Branch = null!
                }).FirstOrDefault();
            if (doctorBranch == null)
            {
                throw new Exception("User not fount: ");
            }
            await context.UserBranches.AddAsync(doctorBranch);
            await context.SaveChangesAsync();
            //-------------------------------nurse ---------------------------------------
            var nurse = users.FirstOrDefault(u => u.Email == "nurse@admin.com");
            if (nurse == null)
            {
                throw new Exception("User not fount: ");
            }

            var nurseBranch = branches
                .Where(b => b.Name == "Cairo Branch")
                .Select(b => new UserBranch
                {
                    UserId = nurse.Id,
                    BranchId = b.Id,
                    User = null!,
                    Branch = null!
                }).FirstOrDefault();
            if (nurseBranch == null)
            {
                throw new Exception("User not fount: ");
            }
            await context.UserBranches.AddAsync(nurseBranch);
            await context.SaveChangesAsync();
            //-------------------------------receptionist ---------------------------------------
            var receptionist = users.FirstOrDefault(u => u.Email == "receptionist@admin.com");
            if (receptionist == null)
            {
                throw new Exception("User not fount: ");
            }

            var receptionistBranch = branches
                .Where(b => b.Name == "Cairo Branch")
                .Select(b => new UserBranch
                {
                    UserId = receptionist.Id,
                    BranchId = b.Id,
                    User = null!,
                    Branch = null!
                }).FirstOrDefault();
            if (receptionistBranch == null)
            {
                throw new Exception("User not fount: ");
            }
            await context.UserBranches.AddAsync(receptionistBranch);
            await context.SaveChangesAsync();
            //-------------------------------accountant ---------------------------------------
            var accountant = users.FirstOrDefault(u => u.Email == "accountant@admin.com");
            if (accountant == null)
            {
                throw new Exception("User not fount: ");
            }

            var accountantBranch = branches
                .Where(b => b.Name == "Cairo Branch")
                .Select(b => new UserBranch
                {
                    UserId = accountant.Id,
                    BranchId = b.Id,
                    User = null!,
                    Branch = null!
                }).FirstOrDefault();
            if (accountantBranch == null)
            {
                throw new Exception("User not fount: ");
            }
            await context.UserBranches.AddAsync(accountantBranch);
            await context.SaveChangesAsync();
            //-------------------------------pharmacist ---------------------------------------
            var pharmacist = users.FirstOrDefault(u => u.Email == "pharmacist@admin.com");
            if (pharmacist == null)
            {
                throw new Exception("User not fount: ");
            }

            var pharmacistBranch = branches
                .Where(b => b.Name == "Cairo Branch")
                .Select(b => new UserBranch
                {
                    UserId = pharmacist.Id,
                    BranchId = b.Id,
                    User = null!,
                    Branch = null!
                }).FirstOrDefault();
            if (pharmacistBranch == null)
            {
                throw new Exception("User not fount: ");
            }
            await context.UserBranches.AddAsync(pharmacistBranch);
            await context.SaveChangesAsync();
            //-------------------------------lab ---------------------------------------
            var lab = users.FirstOrDefault(u => u.Email == "lab@admin.com");
            if (lab == null)
            {
                throw new Exception("User not fount: ");
            }

            var labBranch = branches
                .Where(b => b.Name == "Cairo Branch")
                .Select(b => new UserBranch
                {
                    UserId = lab.Id,
                    BranchId = b.Id,
                    User = null!,
                    Branch = null!
                }).FirstOrDefault();
            if (labBranch == null)
            {
                throw new Exception("User not fount: ");
            }
            await context.UserBranches.AddAsync(labBranch);
            await context.SaveChangesAsync();
            //-------------------------------patient ---------------------------------------
            var patient = users.FirstOrDefault(u => u.Email == "patient@admin.com");
            if (patient == null)
            {
                throw new Exception("User not fount: ");
            }

            var patientBranch = branches
                .Where(b => b.Name == "Cairo Branch")
                .Select(b => new UserBranch
                {
                    UserId = patient.Id,
                    BranchId = b.Id,
                    User = null!,
                    Branch = null!
                }).FirstOrDefault();
            if (patientBranch == null)
            {
                throw new Exception("User not fount: ");
            }
            await context.UserBranches.AddAsync(patientBranch);
            await context.SaveChangesAsync();

        }



    }
}
