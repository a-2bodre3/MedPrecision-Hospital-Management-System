using HospitalManagement.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Infrastructure.DataAccess.Configurations.Common
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            //-------------------id-------------------------
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            //------------------street----------------------
            builder.Property(x => x.Street).HasMaxLength(200).IsRequired();
            //------------------city----------------------
            builder.Property(x => x.City).HasMaxLength(200).IsRequired();
            //------------------country----------------------
            builder.Property(x => x.Country).HasMaxLength(200).IsRequired();

            //------------------relation----------------------
            builder.Property(x => x.AddressType)
                .HasConversion<string>()
                .IsRequired();

        }

    }
}
