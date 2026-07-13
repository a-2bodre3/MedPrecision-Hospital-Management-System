using HospitalManagement.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Infrastructure.DataAccess.Configurations.Identity
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            //----------------id--------------------------------
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();


            //---------------token------------------------------
            builder.Property(x => x.Token)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(x => x.Token).IsUnique();

            //-------------description---------------------------
            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(255);

            //-----------module-------------------------------------
            builder.Property(x => x.Module).IsRequired().HasMaxLength(100);

            //--------------relation --------------------------------
            builder.HasMany(x => x.RolePermission)
                .WithOne(x => x.Permission)
                .HasForeignKey(x => x.PermissionId);
        }
    }
}
