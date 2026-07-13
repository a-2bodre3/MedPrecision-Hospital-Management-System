using HospitalManagement.Domain.Entities.Staff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Infrastructure.DataAccess.Configurations.Staff
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            //-------------------id-------------------------
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            //-----------------job title-------------------
            builder.Property(x => x.JobTitle).HasMaxLength(100).IsRequired();

            //----------------salary--------------------
            builder.Property(x => x.Salary).HasColumnType("decimal(18,2)").IsRequired();

            //-----------------relation with user-------------------
            builder.HasOne(e => e.User)
                .WithOne(u => u.Employee)
                .HasForeignKey<Employee>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //----------------- relation with department-------------
            builder.HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            //---------------- relation with Address-----------------
            builder.HasOne(e => e.Address)
                .WithOne()
                .HasForeignKey<Employee>(e => e.AddressId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
