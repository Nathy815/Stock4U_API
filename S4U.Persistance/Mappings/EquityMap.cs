using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using S4U.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Persistance.Mappings
{
    public class EquityMap : IEntityTypeConfiguration<Equity>
    {
        public void Configure(EntityTypeBuilder<Equity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Ticker)
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(e => e.Name)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.HasIndex(e => new { e.Id, e.Ticker });
        }
    }
}