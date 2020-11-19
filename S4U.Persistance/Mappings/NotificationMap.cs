using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using S4U.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Persistance.Mappings
{
    public class NotificationMap : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(e => e.Body)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.HasOne(e => e.User)
                   .WithMany(e => e.Notifications)
                   .HasForeignKey(e => e.UserID)
                   .IsRequired();

            builder.HasIndex(e => new { e.Id, e.UserID });
        }
    }
}