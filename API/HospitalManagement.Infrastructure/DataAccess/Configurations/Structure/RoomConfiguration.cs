using HospitalManagement.Domain.Entities.Structure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Infrastructure.DataAccess.Configurations.Structure
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            //-------------------id-------------------------
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            //-----------------room number-----------------
            builder.Property(x => x.RoomNumber).HasMaxLength(20).IsRequired();

            builder.HasIndex(x => new { x.BranchId, x.RoomNumber }).IsUnique();

            //----------------Floor ------------------------
            builder.Property(x => x.Floor).IsRequired();

            //--------------relation with branch---------------
            builder.HasOne(r => r.Branch)
                .WithMany(b => b.Rooms)
                .HasForeignKey(r => r.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            //--------------relation with department---------------
            builder.HasOne(r => r.Department)
                .WithMany(d => d.Rooms)
                .HasForeignKey(r => r.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
