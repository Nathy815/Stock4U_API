using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using S4U.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Persistance.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(e => e.Email)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(e => e.Address)
                   .HasMaxLength(256);

            builder.Property(e => e.Number)
                   .HasMaxLength(6);

            builder.Property(e => e.Compliment)
                   .HasMaxLength(30);

            builder.Property(e => e.Gender)
                   .HasMaxLength(11);

            builder.Property(e => e.Image)
                   .HasMaxLength(256);

            builder.HasOne(e => e.Role)
                   .WithMany(e => e.Users)
                   .HasForeignKey(e => e.RoleID)
                   .IsRequired();

            builder.HasOne(e => e.Signature)
                   .WithOne(e => e.User)
                   .HasForeignKey<Signature>(e => e.Id);

            builder.HasIndex(e => new { e.Id, e.Email })
                   .IsUnique();
        }
    }
}
