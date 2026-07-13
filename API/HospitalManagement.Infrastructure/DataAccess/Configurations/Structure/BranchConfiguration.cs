using HospitalManagement.Domain.Entities.Structure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Infrastructure.DataAccess.Configurations.Structure
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            //--------------id---------------------
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            //-----------name ----------------------
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.HasIndex(x => x.Name).IsUnique();

            //------------code----------------------
            builder.Property(x => x.Code).IsRequired().HasMaxLength(20);
            builder.HasIndex(x => x.Code).IsUnique();

            //-------------is active----------------
            builder.Property(x => x.IsActive).IsRequired();

            //-------------phone number----------------
            builder.Property(x => x.PhoneNumber).IsRequired();
            builder.HasIndex(x => x.PhoneNumber).IsUnique();

            //-----------relation with User-------------------
            builder.HasMany(x => x.UserBranches)
                .WithOne(x => x.Branch)
                .HasForeignKey(x => x.BranchId);

            //-----------relation with Address-------------------
            builder.HasOne(x => x.Address)
                .WithOne()
                .HasForeignKey<Branch>(x => x.AddressId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
