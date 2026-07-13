using HospitalManagement.Domain.Entities.Staff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Infrastructure.DataAccess.Configurations.Staff
{
    public class SubSpecialtyConfiguration : IEntityTypeConfiguration<SubSpecialty>
    {
        public void Configure(EntityTypeBuilder<SubSpecialty> builder)
        {
            //-------------------id-------------------------
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            //-----------------name-----------------------
            builder.HasIndex(n => new { n.Name, n.SpecializationId }).IsUnique();
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

            //--------------relation with specialization----------------
            builder.HasOne(s => s.Specialization)
                .WithMany(s => s.SubSpecialties)
                .HasForeignKey(s => s.SpecializationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
