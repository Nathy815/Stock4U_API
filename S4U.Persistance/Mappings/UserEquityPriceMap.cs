using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using S4U.Domain.Entities;
using S4U.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Persistance.Mappings
{
    public class UserEquityPriceMap : IEntityTypeConfiguration<UserEquityPrice>
    {
        public void Configure(EntityTypeBuilder<UserEquityPrice> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Price)
                   .IsRequired();

            builder.Property(e => e.Type)
                   .HasConversion(e => e.ToString(),
                                  e => (ePriceType)Enum.Parse(typeof(ePriceType), e))
                   .IsRequired();

            builder.HasOne(e => e.UserEquity)
                   .WithMany(e => e.Prices)
                   .HasForeignKey(e => new { e.UserID, e.EquityID })
                   .IsRequired();

            builder.HasIndex(e => new { e.Id, e.UserID, e.EquityID });
        }
    }
}