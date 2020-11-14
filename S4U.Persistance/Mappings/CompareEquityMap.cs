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
            /*builder.HasKey(e => new { e.PrincipalID, e.CompareID });

            builder.HasOne(e => e.Principal)
                   .WithMany(e => e.PrincipalEquities)
                   .HasForeignKey(e => e.PrincipalID)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            builder.HasOne(e => e.Compare)
                   .WithMany(e => e.EquitiesToCompare)
                   .HasForeignKey(e => e.CompareID)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            builder.HasIndex(e => new { e.PrincipalID, e.CompareID });*/
        }
    }
}
