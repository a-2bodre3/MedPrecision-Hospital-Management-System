using HospitalManagement.Domain.Entities.Staff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Infrastructure.DataAccess.Configurations.Staff
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            //-------------------id-------------------------
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            //--------------licenseNumber--------------------
            builder.HasIndex(d => d.LicenseNumber).IsUnique();
            builder.Property(x => x.LicenseNumber).HasMaxLength(50).IsRequired();

            //---------------consultation fee----------------------
            builder.Property(x => x.ConsultationFee).HasColumnType("decimal(18,2)").IsRequired();

            //--------------- relation with employee------------
            builder.HasOne(d => d.Employee)
                .WithOne(e => e.Doctor)
                .HasForeignKey<Doctor>(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            //---------------relation with subSpecialty--------------
            builder.HasOne(d => d.SubSpecialty)
                .WithMany()
                .HasForeignKey(d => d.SubSpecialtyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
