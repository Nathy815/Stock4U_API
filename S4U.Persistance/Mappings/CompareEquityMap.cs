using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using S4U.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Persistance.Mappings
{
    public class CompareEquityMap : IEntityTypeConfiguration<CompareEquity>
    {
        public void Configure(EntityTypeBuilder<CompareEquity> builder)
        {
            builder.HasKey(e => new { e.UserID, e.EquityID, e.CompareID });

            builder.HasOne(e => e.Equity)
                   .WithMany(e => e.EquitiesThatCompare)
                   .HasForeignKey(e => e.CompareID)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            builder.HasOne(e => e.UserEquity)
                   .WithMany(e => e.EquitiesToCompare)
                   .HasForeignKey(e => new { e.UserID, e.EquityID })
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            builder.HasIndex(e => new { e.UserID, e.EquityID, e.CompareID });
        }
    }
}
