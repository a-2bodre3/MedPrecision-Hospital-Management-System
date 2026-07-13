using HospitalManagement.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Infrastructure.DataAccess.Configurations.Identity
{
    public class UserBranchConfiguration : IEntityTypeConfiguration<UserBranch>
    {
        public void Configure(EntityTypeBuilder<UserBranch> builder)
        {
            // ---------------- Primary Key (Composite Key) ----------------
            builder.HasKey(x => new { x.UserId, x.BranchId });

            // ---------------- User Relationship ----------------
            builder.HasOne(x => x.User)
                .WithMany(x => x.UserBranches)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ---------------- Branch Relationship ----------------
            builder.HasOne(x => x.Branch)
                .WithMany(x => x.UserBranches)
                .HasForeignKey(x => x.BranchId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
