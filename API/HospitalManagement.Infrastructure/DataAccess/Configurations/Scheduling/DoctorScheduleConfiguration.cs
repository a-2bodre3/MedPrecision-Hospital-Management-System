using HospitalManagement.Domain.Entities.Scheduling;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace HospitalManagement.Infrastructure.DataAccess.Configurations.Scheduling
{
    public class DoctorScheduleConfiguration : IEntityTypeConfiguration<DoctorSchedule>
    {
        public void Configure(EntityTypeBuilder<DoctorSchedule> builder)
        {

            builder.HasKey(ds => ds.Id);
            builder.Property(ds => ds.Id).ValueGeneratedOnAdd();


            builder.Property(ds => ds.DayOfWeeks)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<List<DayOfWeek>>(v, (JsonSerializerOptions)null)
                )
                .Metadata.SetValueComparer(new Microsoft.EntityFrameworkCore.ChangeTracking.ValueComparer<List<DayOfWeek>>(
        (c1, c2) => c1.SequenceEqual(c2),
        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
        c => c.ToList()
    ));


            builder.Property(ds => ds.StartTime)
                .HasColumnType("time")
                .IsRequired();

            builder.Property(ds => ds.EndTime)
                .HasColumnType("time")
                .IsRequired();


            builder.Property(ds => ds.MaxPatients)
                .IsRequired();

            builder.Property(ds => ds.ValidFrom)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(ds => ds.ValidUntil)
                .HasColumnType("datetime2")
                .IsRequired(false); 

            builder.Property(ds => ds.IsActive)
                .HasDefaultValue(true);


            builder.Property(ds => ds.CreatedAt).HasColumnType("datetime2").IsRequired();
            builder.Property(ds => ds.CreatedById).HasMaxLength(100).IsRequired(false);
            builder.Property(ds => ds.LastModifiedAt).HasColumnType("datetime2").IsRequired(false);
            builder.Property(ds => ds.LastModifiedById).HasMaxLength(100).IsRequired(false);

  
            builder.Property(ds => ds.IsDeleted).HasDefaultValue(false);
            builder.Property(ds => ds.DeletedAt).HasColumnType("datetime2").IsRequired(false);
            builder.Property(ds => ds.DeletedById).HasMaxLength(100).IsRequired(false);




            builder.Property(ds => ds.DoctorId).IsRequired();
            builder.Property(ds => ds.RoomId).IsRequired();

            builder.HasOne(ds => ds.Doctor)
                .WithMany(d => d.DoctorSchedules)
                .HasForeignKey(ds => ds.DoctorId)
                .OnDelete(DeleteBehavior.Restrict); 


            builder.HasOne(ds => ds.Room)
                .WithMany(r => r.DoctorSchedules)
                .HasForeignKey(ds => ds.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}