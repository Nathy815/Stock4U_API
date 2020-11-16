using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using S4U.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Persistance.Mappings
{
    public class NoteMap : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(e => e.Comments)
                   .HasMaxLength(256);

            builder.Property(e => e.Attach)
                   .HasMaxLength(256);

            builder.HasOne(e => e.UserEquity)
                   .WithMany(e => e.Notes)
                   .HasForeignKey(e => new { e.UserID, e.EquityID })
                   .IsRequired();

            builder.HasIndex(e => new { e.Id, e.UserID, e.EquityID, e.Alert });
        }
    }
}