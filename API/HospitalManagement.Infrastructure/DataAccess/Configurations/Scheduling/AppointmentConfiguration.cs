using HospitalManagement.Domain.Entities.Scheduling;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Infrastructure.DataAccess.Configurations.Scheduling
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.AppointmentDate)
                .IsRequired();

            builder.Property(a => a.CreatedAt)
                .IsRequired();


            builder.Property(a => a.Type)
                .IsRequired();

            builder.Property(a => a.Status)
                .IsRequired();

            builder.Property(a => a.PaymentMethod)
                .IsRequired();

            builder.Property(a => a.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(a => a.Notes)
                .HasMaxLength(500);


            builder.HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.DoctorSchedule)
                .WithMany(ds => ds.Appointments)
                .HasForeignKey(a => a.DoctorScheduleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
