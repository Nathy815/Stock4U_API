using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using S4U.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Persistance.Mappings
{
    public class PlanMap : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(e => e.Descyption)
                   .HasMaxLength(500);

            builder.HasIndex(e => new { e.Id, e.Name });
        }
    }
}