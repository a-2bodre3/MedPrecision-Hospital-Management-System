using HospitalManagement.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Infrastructure.DataAccess.Configurations.Identity
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            // ---------------- Primary Key (Composite Key) ----------------
            builder.HasKey(x => new { x.RoleId, x.PermissionId });

            // ---------------- Role Relationship ----------------
            builder.HasOne(x => x.Role)
                .WithMany(x => x.UserPermission)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            // ---------------- permission Relationship ----------------
            builder.HasOne(x => x.Permission)
                .WithMany(x => x.RolePermission)
                .HasForeignKey(x => x.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
