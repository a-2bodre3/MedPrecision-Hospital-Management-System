using HospitalManagement.Domain.Entities.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Infrastructure.DataAccess.Configurations.Patients
{
    public class PatientChronicDiseaseConfiguration : IEntityTypeConfiguration<PatientChronicDisease>
    {
        public void Configure(EntityTypeBuilder<PatientChronicDisease> builder)
        {
            //--------------composite key---------------------
            builder.HasKey(x => new { x.PatientId, x.ChronicDiseaseId });

            //--------------relation--------------------
            builder.HasOne(x => x.Patient)
                .WithMany(x => x.PatientChronicDiseases)
                .HasForeignKey(x => x.PatientId);

            builder.HasOne(x => x.ChronicDisease)
                .WithMany(x => x.PatientChronicDiseases)
                .HasForeignKey(x => x.ChronicDiseaseId);
        }
    }
}
