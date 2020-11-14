using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using S4U.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Persistance.Mappings
{
    public class UserEquityMap : IEntityTypeConfiguration<UserEquity>
    {
        public void Configure(EntityTypeBuilder<UserEquity> builder)
        {
            builder.HasKey(e => new { e.UserID, e.EquityID });

            builder.HasOne(e => e.User)
                   .WithMany(e => e.UsersEquities)
                   .HasForeignKey(e => e.UserID)
                   .IsRequired();

            builder.HasOne(e => e.Equity)
                   .WithMany(e => e.UsersEquities)
                   .HasForeignKey(e => e.EquityID)
                   .IsRequired();

            builder.HasIndex(e => new { e.UserID, e.EquityID });
        }
    }
}