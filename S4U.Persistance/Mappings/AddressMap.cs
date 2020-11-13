using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using S4U.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Persistance.Mappings
{
    public class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.ZipCode)
                   .HasMaxLength(8)
                   .IsRequired();

            builder.Property(e => e.Local)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(e => e.Number)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(e => e.Compliment)
                   .HasMaxLength(150);

            builder.Property(e => e.City)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(e => e.State)
                   .HasMaxLength(2)
                   .IsRequired();

            builder.Property(e => e.Country)
                   .HasMaxLength(100);

            builder.HasIndex(e => new { e.Id, e.ZipCode });

        }
    }
}
