using HospitalManagement.Domain.Entities.Structure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Infrastructure.DataAccess.Configurations.Structure
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            //------------------id----------------
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            //-----------name ----------------------
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.HasIndex(x => new { x.BranchId, x.Name }).IsUnique();
            //----------description-----------------
            builder.Property(e => e.Description).HasMaxLength(300);
            //-------------is active----------------
            builder.Property(x => x.IsActive).IsRequired();
            //-------------relation with branch--------
            builder.HasOne(d => d.Branch)
                .WithMany(b => b.Departments)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
