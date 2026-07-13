using HospitalManagement.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Infrastructure.DataAccess.Configurations.Identity
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //------------id-----------------------------------------
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            //------------email---------------------------------------
            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(155);

            builder.HasIndex(x => x.Email)
                .IsUnique();
            // ---------------- Password ----------------
            builder.Property(x => x.PasswordHash)
                .IsRequired();

            // ---------------- Name ----------------
            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);

            // ---------------- Phone ----------------
            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(20);

            builder.HasIndex(x => x.PhoneNumber).IsUnique();

            // ---------------- IsActive ----------------
            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            // ---------------- CreatedAt ----------------
            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            //--------------relation--------------------
            builder.HasMany(x => x.UserRoles)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.UserBranches)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
    }
}
