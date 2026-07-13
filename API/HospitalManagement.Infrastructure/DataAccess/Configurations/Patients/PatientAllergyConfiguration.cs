using HospitalManagement.Domain.Entities.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Infrastructure.DataAccess.Configurations.Patients
{
    public class PatientAllergyConfiguration : IEntityTypeConfiguration<PatientAllergy>
    {
        public void Configure(EntityTypeBuilder<PatientAllergy> builder)
        {
            //--------------composite key---------------------
            builder.HasKey(x => new { x.PatientId, x.AllergyId });

            //--------------relation--------------------
            builder.HasOne(x => x.Patient)
                .WithMany(x => x.PatientAllergies)
                .HasForeignKey(x => x.PatientId);

            builder.HasOne(x => x.Allergy)
                .WithMany(x => x.PatientAllergies)
                .HasForeignKey(x => x.AllergyId);
        }
    }
}
